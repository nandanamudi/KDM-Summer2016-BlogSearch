using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for program
/// </summary>
namespace BloggerMongoDB1
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;


   [BsonIgnoreExtraElements]
    public class program
    {
        private ObjectId id;
        [BsonRepresentation(BsonType.String)] 
        private string fname;
        [BsonRepresentation(BsonType.String)] 
        private string lname;
        [BsonRepresentation(BsonType.String)] 
        private string username;
        [BsonRepresentation(BsonType.String)] 
        private string password;
        [BsonRepresentation(BsonType.String)] 
        private string emailid;
        [BsonRepresentation(BsonType.String)] 
        private string age;
        [BsonRepresentation(BsonType.String)] 
        private string phno;


        public ObjectId Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Fname
        {
            get { return fname; }
            set { fname = value; }
        }

        public string Lname
        {
            get { return lname; }
            set { lname = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Emailid
        {
            get { return emailid; }
            set { emailid = value; }
        }

        public string Age
        {
            get { return age; }
            set { age = value; }
        }

        public string Phno
        {
            get { return phno; }
            set { phno = value; }
        }
    }
}
	
