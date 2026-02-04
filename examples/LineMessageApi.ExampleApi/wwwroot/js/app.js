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
        setEndpoint: true
      },
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
    }
  },
  mounted() {
    // 初始化表單預設值
    if (!this.form.webhookUrl) {
      this.form.webhookUrl = `${window.location.origin}/dashboard/hook`;
    }
    // 載入設定與事件
    this.fetchConfig();
    this.refreshInfo();
    this.fetchEvents();
  },
  methods: {
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
          this.events = data;
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
        this.events.unshift(eventRecord);
        if (this.events.length > 200) {
          this.events = this.events.slice(0, 200);
        }
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
        })
        .catch(() => {
          this.hubConnection = null;
          this.connectionState = 'disconnected';
        });
    }
  }
}).mount('#app');
