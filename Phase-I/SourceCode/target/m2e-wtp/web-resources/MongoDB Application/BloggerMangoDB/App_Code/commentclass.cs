using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

[BsonIgnoreExtraElements]
public class commentclass
{
    [BsonRepresentation(BsonType.ObjectId)]
    private ObjectId id;

    [BsonRepresentation(BsonType.ObjectId)]
    private ObjectId postid;

    [BsonRepresentation(BsonType.String)]
    public string username;

    [BsonRepresentation(BsonType.String)]
    public string comment;

    [BsonRepresentation(BsonType.DateTime)]
    public DateTime commentdate;


    public ObjectId Id
    {
        get { return id; }
        set { id = value; }
    }

    public ObjectId Postid
    {
        get { return postid; }
        set { postid = value; }
    }


    public string Username
    {
        get { return username; }
        set { username = value; }
    }

    public string Comment
    {
        get { return comment; }
        set { comment = value; }
    }

    public DateTime Commentdate
    {
        get { return commentdate; }
        set { commentdate = value; }
    }
    
}