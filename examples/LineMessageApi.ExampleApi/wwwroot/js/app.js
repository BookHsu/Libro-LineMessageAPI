// Vue 前端控制器
const { createApp } = Vue;

createApp({
  data() {
    return {
      // 表單資料
      form: {
        channelAccessToken: '',
        channelSecret: '',
        webhookUrl: '',
        setEndpoint: false
      },
      showToken: false,
      showSecret: false,
      // 狀態資料
      status: {
        configured: false,
        webhookUrl: '',
        updatedAtUtc: ''
      },
      botInfo: null,
      webhookEndpoint: null,
      events: [],
      loading: false,
      message: '',
      connectionState: 'disconnected',
      hubConnection: null,
      newEventCount: 0,
      density: 'comfortable',
      isStreamAtTop: true,
      darkMode: false,
      placeholderImage: 'https://placehold.co/88x88?text=LINE'
    };
  },
  computed: {
    // Webhook URL 顯示
    currentWebhookUrl() {
      return this.form.webhookUrl || `${window.location.origin}/dashboard/hook`;
    },
    // 連線狀態樣式
    connectionClass() {
      if (this.connectionState === 'connected') {
        return 'status-online';
      }
      if (this.connectionState === 'connecting') {
        return 'status-warn';
      }
      return 'status-offline';
    },
    // 連線狀態文字
    connectionLabel() {
      if (this.connectionState === 'connected') {
        return 'SignalR 已連線';
      }
      if (this.connectionState === 'connecting') {
        return 'SignalR 連線中';
      }
      return 'SignalR 未連線';
    },
    // Webhook 啟用狀態
    webhookStatusClass() {
      return this.webhookEndpoint?.active ? 'badge bg-success' : 'badge bg-secondary';
    },
    // Webhook 啟用狀態文字
    webhookStatusLabel() {
      return this.webhookEndpoint?.active ? '已啟用' : '未啟用';
    },
    densityClass() {
      return this.density === 'compact' ? 'hook-compact' : 'hook-comfort';
    },
    groupedEvents() {
      const groups = new Map();
      this.events.forEach((eventRecord) => {
        const label = this.getDateLabel(eventRecord?.receivedAtUtc);
        if (!groups.has(label)) {
          groups.set(label, []);
        }
        groups.get(label).push(eventRecord);
      });
      return Array.from(groups.entries()).map(([label, items]) => ({
        label,
        items
      }));
    }
  },
  mounted() {
    // 初始化表單預設值
    if (!this.form.webhookUrl) {
      this.form.webhookUrl = `${window.location.origin}/dashboard/hook`;
    }
    this.loadTheme();
    // 載入設定與事件
    this.fetchConfig();
    this.refreshInfo();
    this.fetchEvents();
    this.initTooltips();
  },
  methods: {
    loadTheme() {
      const saved = localStorage.getItem('line-dashboard-theme');
      this.darkMode = saved === 'dark';
      this.applyTheme();
    },
    applyTheme() {
      document.body.classList.toggle('theme-dark', this.darkMode);
    },
    toggleTheme() {
      this.darkMode = !this.darkMode;
      localStorage.setItem('line-dashboard-theme', this.darkMode ? 'dark' : 'light');
      this.applyTheme();
    },
    toggleTokenVisibility() {
      this.showToken = !this.showToken;
    },
    toggleSecretVisibility() {
      this.showSecret = !this.showSecret;
    },
    // 啟用 Bootstrap Tooltip
    initTooltips() {
      const triggers = document.querySelectorAll('[data-bs-toggle="tooltip"]');
      triggers.forEach((el) => {
        new bootstrap.Tooltip(el);
      });
    },
    // 格式化 UTC 時間
    formatUtc(value) {
      if (!value) {
        return '-';
      }
      try {
        return new Date(value).toLocaleString();
      } catch {
        return value;
      }
    },
    // 取得設定狀態
    async fetchConfig() {
      try {
        const response = await fetch('/dashboard/api/line/config');
        if (!response.ok) {
          return;
        }
        this.status = await response.json();
      } catch {
        // 略過錯誤
      }
    },
    // 取得事件清單
    async fetchEvents() {
      try {
        const response = await fetch('/dashboard/api/line/events');
        if (!response.ok) {
          return;
        }
        const data = await response.json();
        if (Array.isArray(data)) {
          this.events = [...data]
            .map((item) => this.decorateEvent(item));
          this.sortEvents();
          this.newEventCount = 0;
          this.$nextTick(() => this.syncStreamState(true));
        }
      } catch {
        // 略過錯誤
      }
    },
    // 重新載入 Bot 與 Webhook 資訊
    async refreshInfo() {
      try {
        const response = await fetch('/dashboard/api/line/info');
        if (!response.ok) {
          return;
        }
        const data = await response.json();
        this.message = data.message || '';
        if (data.config) {
          this.status = data.config;
        }
        this.botInfo = data.botInfo || null;
        this.webhookEndpoint = data.webhookEndpoint || null;
        this.ensureHubConnection();
      } catch {
        this.message = '取得資訊失敗。';
      }
    },
    getDateLabel(value) {
      if (!value) {
        return '未知時間';
      }
      const date = new Date(value);
      if (Number.isNaN(date.getTime())) {
        return '未知時間';
      }
      const today = new Date();
      const startOfToday = new Date(today.getFullYear(), today.getMonth(), today.getDate());
      const startOfDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
      const diffDays = Math.round((startOfToday - startOfDate) / 86400000);

      if (diffDays === 0) {
        return '今天';
      }
      if (diffDays === 1) {
        return '昨天';
      }
      const month = String(date.getMonth() + 1).padStart(2, '0');
      const day = String(date.getDate()).padStart(2, '0');
      return `${date.getFullYear()}-${month}-${day}`;
    },
    // 儲存設定
    async saveConfig() {
      this.loading = true;
      this.message = '';
      try {
        const response = await fetch('/dashboard/api/line/config', {
          method: 'POST',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify({
            channelAccessToken: this.form.channelAccessToken,
            channelSecret: this.form.channelSecret,
            webhookUrl: this.form.webhookUrl,
            setEndpoint: this.form.setEndpoint
          })
        });

        const data = await response.json();
        this.message = data.message || '';
        if (data.config) {
          this.status = data.config;
        }
        this.botInfo = data.botInfo || null;
        this.webhookEndpoint = data.webhookEndpoint || null;
        this.ensureHubConnection();
      } catch {
        this.message = '設定失敗，請確認網路連線（即時事件仍會透過 SignalR 更新）。';
      } finally {
        this.loading = false;
      }
    },
    // 清除事件
    clearEvents() {
      this.events = [];
      this.newEventCount = 0;
    },
    toggleRaw(eventRecord) {
      eventRecord.expanded = !eventRecord.expanded;
    },
    togglePin(eventRecord) {
      eventRecord.pinned = !eventRecord.pinned;
      this.sortEvents();
    },
    copyRaw(eventRecord) {
      if (!navigator.clipboard) {
        return;
      }
      navigator.clipboard.writeText(eventRecord.rawJson || '')
        .then(() => {
          eventRecord.copied = true;
          setTimeout(() => {
            eventRecord.copied = false;
          }, 1200);
        })
        .catch(() => {});
    },
    toggleDensity() {
      this.density = this.density === 'compact' ? 'comfortable' : 'compact';
    },
    getMessageTypeClass(type) {
      switch (type) {
        case 'text':
          return 'type-text';
        case 'image':
          return 'type-image';
        case 'video':
          return 'type-video';
        case 'audio':
          return 'type-audio';
        case 'file':
          return 'type-file';
        case 'location':
          return 'type-location';
        case 'sticker':
          return 'type-sticker';
        default:
          return 'type-unknown';
      }
    },
    decorateEvent(item) {
      const source = this.extractSource(item?.rawJson);
      return {
        ...item,
        expanded: false,
        pinned: false,
        copied: false,
        sourceIdLabel: source?.label || '',
        sourceIdValue: source?.value || ''
      };
    },
    extractSource(rawJson) {
      if (!rawJson) {
        return null;
      }
      try {
        const payload = JSON.parse(rawJson);
        const source = payload?.events?.[0]?.source;
        if (!source) {
          return null;
        }
        if (source.userId) {
          return { label: 'User ID', value: source.userId };
        }
        if (source.groupId) {
          return { label: 'Group ID', value: source.groupId };
        }
        if (source.roomId) {
          return { label: 'Room ID', value: source.roomId };
        }
        return { label: 'Source', value: source.type || '' };
      } catch {
        return null;
      }
    },
    sortEvents() {
      this.events = [...this.events].sort((a, b) => {
        if (a.pinned !== b.pinned) {
          return a.pinned ? -1 : 1;
        }
        const aTime = a?.receivedAtUtc ? Date.parse(a.receivedAtUtc) : 0;
        const bTime = b?.receivedAtUtc ? Date.parse(b.receivedAtUtc) : 0;
        return bTime - aTime;
      });
    },
    getStreamEl() {
      return this.$el?.querySelector('.hook-stream');
    },
    onStreamScroll() {
      const el = this.getStreamEl();
      if (!el) {
        return;
      }
      const atTop = el.scrollTop <= 8;
      this.isStreamAtTop = atTop;
      if (atTop && this.newEventCount > 0) {
        this.newEventCount = 0;
      }
    },
    syncStreamState(scrollToTop) {
      const el = this.getStreamEl();
      if (!el) {
        return;
      }
      if (scrollToTop) {
        el.scrollTop = 0;
      }
      this.onStreamScroll();
    },
    jumpToLatest() {
      this.$nextTick(() => this.syncStreamState(true));
    },
    // 建立 SignalR 連線
    shouldConnectHub() {
      const endpoint = this.webhookEndpoint?.endpoint;
      if (!endpoint || !this.webhookEndpoint?.active) {
        return false;
      }
      return endpoint === this.currentWebhookUrl;
    },
    ensureHubConnection() {
      if (this.shouldConnectHub()) {
        this.connectHub();
        return;
      }

      if (this.hubConnection) {
        const connection = this.hubConnection;
        this.hubConnection = null;
        connection.stop().finally(() => {
          this.connectionState = 'disconnected';
        });
      }
    },
    connectHub() {
      if (this.hubConnection) {
        return;
      }

      this.connectionState = 'connecting';
      const connection = new signalR.HubConnectionBuilder()
        .withUrl('/hubs/line-webhook')
        .withAutomaticReconnect()
        .build();

      this.hubConnection = connection;
      connection.on('webhookReceived', (eventRecord) => {
        // 新事件插入清單最前
        this.events.unshift(this.decorateEvent(eventRecord));
        if (this.events.length > 200) {
          this.events = this.events.slice(0, 200);
        }
        this.sortEvents();
        this.$nextTick(() => {
          if (this.isStreamAtTop) {
            this.syncStreamState(true);
          } else {
            this.newEventCount += 1;
          }
        });
      });

      connection.onreconnecting(() => {
        this.connectionState = 'connecting';
      });

      connection.onreconnected(() => {
        this.connectionState = 'connected';
      });

      connection.onclose(() => {
        this.connectionState = 'disconnected';
      });

      connection.start()
        .then(() => {
          this.connectionState = 'connected';
          this.fetchEvents();
        })
        .catch(() => {
          this.hubConnection = null;
          this.connectionState = 'disconnected';
        });
    }
  }
}).mount('#app');
