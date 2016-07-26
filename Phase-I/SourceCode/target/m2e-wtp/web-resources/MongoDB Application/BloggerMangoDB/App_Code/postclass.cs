using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class postclass
{
    [BsonRepresentation(BsonType.ObjectId)]
    private ObjectId id;

    [BsonRepresentation(BsonType.String)]
    private string username;

    [BsonRepresentation(BsonType.String)]
    private string fullname;

    [BsonRepresentation(BsonType.String)]
    private string title;

    [BsonRepresentation(BsonType.String)]
    private string post;

    [BsonRepresentation(BsonType.DateTime)]
    private DateTime postdate;

     [BsonRepresentation(BsonType.String)]
     public string[] tags { get; set; }

  //   [BsonRepresentation(BsonType.Document)]
 //   public IList<comment> comments { get; set; }

   


    public ObjectId Id
    {
        get { return id; }
        set { id = value; }
    }

    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    public string Fullname
    {
        get { return fullname; }
        set { fullname = value; }
    }

    public string Title
    {
        get { return title; }
        set { title = value; }
    }

    public string Post
    {
        get { return post; }
        set { post = value; }
    }

    public DateTime Date
    {
        get { return postdate; }
        set { postdate = value; }
    }

   
}

public class comment
{
    [BsonElementAttribute("username")]
    public string Username;
    [BsonElementAttribute("commentmsg")]
    public string CommentMsg;
    [BsonElementAttribute("date")]
    public DateTime Date;
}
    

	
