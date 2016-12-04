using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillsKit.Speechlet;
using AlexaSkillsKit.Slu;
using AlexaSkillsKit.UI;
using System.Web;

namespace PiAzureAlexa.Skills.Host.Speechlets
{
    using Data;
    using Models;

    public class PiAzureAlexaSpeechlet : Speechlet
    {
        public override SpeechletResponse OnIntent(IntentRequest intentRequest, Session session)
        {
            return GetMyNumberResponse();
        }

        public override SpeechletResponse OnLaunch(LaunchRequest launchRequest, Session session)
        {
            return GetWelcomeResponse();
        }

        public override void OnSessionEnded(SessionEndedRequest sessionEndedRequest, Session session)
        {
            throw new NotImplementedException();
        }

        public override void OnSessionStarted(SessionStartedRequest sessionStartedRequest, Session session)
        {
            //throw new NotImplementedException();
        }

        private SpeechletResponse GetWelcomeResponse()
        {
            var speechOutput = "Welcome to my skills.  How may I help you?";

            return BuildSpeechletResponse("Welcome", speechOutput, false);
        }

        private SpeechletResponse GetMyNumberResponse()
        {
            // use document db client to get document and return response
            var myNumberSpeech = string.Empty;
            
            DocumentDBClient.AuthorizationKey = "YOUR AUTHORIZATION KEY";
            DocumentDBClient.DatabaseLink = new Uri("YOUR DATABASE LINK");
            DocumentDBClient.DatabaseName = "YOUR DATABASE NAME";
            DocumentDBClient.DatabaseCollectionName = "YOUR COLLECTION NAME";

            var myNumberDocument = DocumentDBClient.GetDbAsync<MyNumber>("MyNumber-1").Result;

            myNumberSpeech = $"the next number is {myNumberDocument.SomeValue}, dated {myNumberDocument.TimeStamp.ToString("F")}";

            return BuildSpeechletResponse("My Number", myNumberSpeech, true);
        }

        private SpeechletResponse BuildSpeechletResponse(string title, string output, bool shouldSessionEnd)
        {
            // Create the Simple card content.
            SimpleCard card = new SimpleCard();
            card.Title = String.Format("Sample - {0}", title);
            card.Content = String.Format("Sample - {0}", output);

            // Create the plain text output.
            PlainTextOutputSpeech speech = new PlainTextOutputSpeech();
            speech.Text = output;

            // Create the speechlet response.
            SpeechletResponse response = new SpeechletResponse();
            response.ShouldEndSession = shouldSessionEnd;
            response.OutputSpeech = speech;
            response.Card = card;
            return response;
        }
    }
}