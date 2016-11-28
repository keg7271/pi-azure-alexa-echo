using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillsKit.Speechlet;

namespace PiAzureAlexa.Skills.Speechlets
{
    public class PiAzureAlexaSpeechlet : Speechlet
    {
        public override SpeechletResponse OnIntent(IntentRequest intentRequest, Session session)
        {
            throw new NotImplementedException();
        }

        public override SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session)
        {
            throw new NotImplementedException();
        }

        public override void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session)
        {
            throw new NotImplementedException();
        }

        public override void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session)
        {
            throw new NotImplementedException();
        }
    }
}
