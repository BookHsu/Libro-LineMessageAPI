using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Libro.LineMessageAPI.ExampleApi.Services;
using Libro.LineMessageApi.LineMessageObject;
using Libro.LineMessageApi.Services;
using Libro.LineMessageApi.Types;

namespace Libro.LineMessageAPI.ExampleApi.Tests
{
    internal sealed class StubLineSdkFactory : ILineSdkFactory
    {
        public ILineSdkFacade BotWebhookSdk { get; set; }
        public ILineSdkFacade MessageSdk { get; set; }
        public Exception BotWebhookException { get; set; }
        public Exception MessageException { get; set; }

        public ILineSdkFacade CreateBotWebhookSdk(string channelAccessToken)
        {
            if (BotWebhookException != null)
            {
                throw BotWebhookException;
            }

            return BotWebhookSdk ?? new StubLineSdkFacade();
        }

        public ILineSdkFacade CreateMessageSdk(string channelAccessToken)
        {
            if (MessageException != null)
            {
                throw MessageException;
            }

            return MessageSdk ?? new StubLineSdkFacade();
        }
    }

    internal sealed class StubLineSdkFacade : ILineSdkFacade
    {
        public IBotService Bot { get; set; }
        public IWebhookEndpointService WebhookEndpoints { get; set; }
        public IMessageService Messages { get; set; }
    }

    internal sealed class StubBotService : IBotService
    {
        public BotInfo BotInfo { get; set; }

        public BotInfo GetBotInfo()
        {
            return BotInfo ?? new BotInfo();
        }

        public Task<BotInfo> GetBotInfoAsync()
        {
            return Task.FromResult(GetBotInfo());
        }

        public GroupSummary GetGroupSummary(string groupId) => throw new NotSupportedException();
        public Task<GroupSummary> GetGroupSummaryAsync(string groupId) => throw new NotSupportedException();
        public RoomSummary GetRoomSummary(string roomId) => throw new NotSupportedException();
        public Task<RoomSummary> GetRoomSummaryAsync(string roomId) => throw new NotSupportedException();
        public MemberIdsResponse GetGroupMemberIds(string groupId) => throw new NotSupportedException();
        public Task<MemberIdsResponse> GetGroupMemberIdsAsync(string groupId) => throw new NotSupportedException();
        public MemberIdsResponse GetRoomMemberIds(string roomId) => throw new NotSupportedException();
        public Task<MemberIdsResponse> GetRoomMemberIdsAsync(string roomId) => throw new NotSupportedException();
        public MemberCountResponse GetGroupMemberCount(string groupId) => throw new NotSupportedException();
        public Task<MemberCountResponse> GetGroupMemberCountAsync(string groupId) => throw new NotSupportedException();
        public MemberCountResponse GetRoomMemberCount(string roomId) => throw new NotSupportedException();
        public Task<MemberCountResponse> GetRoomMemberCountAsync(string roomId) => throw new NotSupportedException();
    }

    internal sealed class StubWebhookEndpointService : IWebhookEndpointService
    {
        public WebhookEndpointResponse WebhookEndpoint { get; set; }
        public bool SetEndpointResult { get; set; } = true;

        public WebhookEndpointResponse GetWebhookEndpoint()
        {
            return WebhookEndpoint ?? new WebhookEndpointResponse();
        }

        public Task<WebhookEndpointResponse> GetWebhookEndpointAsync()
        {
            return Task.FromResult(GetWebhookEndpoint());
        }

        public bool SetWebhookEndpoint(WebhookEndpointRequest request)
        {
            return SetEndpointResult;
        }

        public Task<bool> SetWebhookEndpointAsync(WebhookEndpointRequest request)
        {
            return Task.FromResult(SetEndpointResult);
        }

        public WebhookTestResponse TestWebhookEndpoint() => new WebhookTestResponse();
        public Task<WebhookTestResponse> TestWebhookEndpointAsync() => Task.FromResult(new WebhookTestResponse());
    }

    internal sealed class StubMessageService : IMessageService
    {
        public int SendReplyAsyncCallCount { get; private set; }
        public string LastReplyToken { get; private set; }
        public Message[] LastMessages { get; private set; }
        public bool ThrowOnSendReplyAsync { get; set; }

        public byte[] GetMessageContent(string messageId) => throw new NotSupportedException();
        public Task<byte[]> GetMessageContentAsync(string messageId) => throw new NotSupportedException();
        public string SendReplyMessage(string replyToken, params Message[] message) => throw new NotSupportedException();

        public Task<string> SendReplyMessageAsync(string replyToken, params Message[] message)
        {
            if (ThrowOnSendReplyAsync)
            {
                throw new InvalidOperationException("boom");
            }

            SendReplyAsyncCallCount++;
            LastReplyToken = replyToken;
            LastMessages = message;
            return Task.FromResult("ok");
        }

        public string SendPushMessage(string toId, params Message[] message) => throw new NotSupportedException();
        public Task<string> SendPushMessageAsync(string toId, params Message[] message) => throw new NotSupportedException();
        public string SendMulticastMessage(List<string> toIds, params Message[] message) => throw new NotSupportedException();
        public Task<string> SendMulticastMessageAsync(List<string> toIds, params Message[] message) => throw new NotSupportedException();
    }
}
