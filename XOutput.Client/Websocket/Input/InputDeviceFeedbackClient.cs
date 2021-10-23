﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XOutput.Serialization;

namespace XOutput.Websocket.Input
{
    public class InputDeviceFeedbackClient : WebsocketJsonClient
    {
        public event InputDeviceFeedbackReceived FeedbackReceived;

        public InputDeviceFeedbackClient(MessageReader messageReader, MessageWriter messageWriter, WebSocketHelper webSocketHelper, Uri baseUri) : base(messageReader, messageWriter, webSocketHelper, baseUri)
        {

        }

        public async Task ConnectAsync(string id)
        {
            await ConnectAsync("InputDevice/" + id);
        }

        protected override void ProcessMessage(MessageBase message)
        {
            if (message is InputDeviceInputResponse)
            {
                var feedbackMessage = message as InputDeviceInputResponse;
                FeedbackReceived?.Invoke(this, new InputDeviceFeedbackReceivedEventArgs
                {
                    InputValues = feedbackMessage.Sources.Select(s => new InputDeviceSourceValue { Id = s.Id, Value = s.Value }).ToList(),
                    ForceFeedbacks = feedbackMessage.Targets.Select(t => new InputDeviceTargetValue { Id = t.Id, Value = t.Value }).ToList(),
                });
            }
        }

        protected Task SendInputAsync(InputDeviceInputRequest message)
        {
            return SendAsync(message);
        }
    }

    public delegate void InputDeviceFeedbackReceived(object sender, InputDeviceFeedbackReceivedEventArgs args);

    public class InputDeviceFeedbackReceivedEventArgs
    {
        public List<InputDeviceSourceValue> InputValues { get; set; }
        public List<InputDeviceTargetValue> ForceFeedbacks { get; set; }
    }
}