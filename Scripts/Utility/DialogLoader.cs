using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DialogLoader
{
    public List<MCA> _mca = new List<MCA>();
    public List<Dialogue> _dialogue = new List<Dialogue>();


    Speaker FindSpeakerWithID(List<Speaker> speakers, string idToCheck)
    {
        foreach(Speaker sp in speakers)
        {
            if (sp._nameID.Equals(idToCheck)) return sp;
        }
        return null;
    }
        
    public DialogLoader(List<Speaker> availableSpeakers)
	{
        /*
        XmlDocument reader = new XmlDocument();
        string xmlPath = Application.dataPath + "/Resources/dialog.xml";

        reader.Load(xmlPath);
        */
        TextAsset textAsset = (TextAsset)Resources.Load("dialog1");
        XmlDocument reader = new XmlDocument();
        reader.LoadXml(textAsset.text);

        List<Dialogue> dialogue = new List<Dialogue>();
        List<MCA> mca = new List<MCA>();
        foreach (XmlNode DialogNode in reader.ChildNodes[0].ChildNodes)
        {
            if (DialogNode.LocalName == "dialog")
            {
                Speaker leftie = FindSpeakerWithID(availableSpeakers, DialogNode["left"].InnerText);
                Speaker rightie = FindSpeakerWithID(availableSpeakers, DialogNode["right"].InnerText);

                dialogue.Add(new Dialogue(DialogNode["ID"].InnerText, ParseSentences(DialogNode), DialogNode["nextID"].InnerText, leftie, rightie));
            }
            if (DialogNode.Name == "MCA")
            {
                List<Choice> options = ParseAnswers(DialogNode);

                mca.Add(new MCA(DialogNode["ID"].InnerText, options));
            }
        }


        _dialogue = dialogue;
        _mca = mca;
    }

    static public List<Choice> ParseAnswers(XmlNode node)
    {
        List<Choice> _ret = new List<Choice>();

        foreach (XmlNode choiceNode in node.ChildNodes)
        {
            if (choiceNode.Name == "answer")
            {
                _ret.Add(new Choice(choiceNode["text"].InnerText, int.Parse(choiceNode["value"].InnerText), choiceNode["nextDialogID"].InnerText));
            }
        }

        return _ret;
    }

    static public List<Sentence> ParseSentences(XmlNode node)
    {
        List<Sentence> _ret = new List<Sentence>();
        foreach (XmlNode speechNode in node.ChildNodes)
        {
            if (speechNode.Name == "speech")
            {
                int position = 0;

                if (speechNode["side"].InnerText == "Right")
                    position = 1;

                Emotion emo = ParseEmotion(speechNode["emotion"].InnerText);

                _ret.Add(new Sentence(position, speechNode["text"].InnerText, ParseEmotion(speechNode["emotion"].InnerText)));
            }
        }
        return _ret;
    }

    static public Emotion ParseEmotion(string Parsable)
    {
        switch (Parsable)
        {
            case "Angry":
                return Emotion.Angry;

            case "Neutral":
                return Emotion.Neutral;

            case "Surprised":
                return Emotion.Surprised;

            case "Confused":
                return Emotion.Confused;

            case "Happy":
                return Emotion.Happy;

            default:
                return Emotion.Neutral;
        }
    }
}
