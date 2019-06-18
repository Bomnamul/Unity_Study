using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostData
{
    public Image ProfilePic { get; set; }
    public string FriendName { get; set; }
    public string Location { get; set; }
    public Image Photo { get; set; }
    public string Likes { get; set; }
    public string Description { get; set; }
}

public class AppDataManager : Singleton<AppDataManager>
{
    protected AppDataManager() { }

    List<PostData> posts = new List<PostData>();
    int timeStamp = 0;
    
    public void AddPost(Image profilePic, 
                        string friendName, 
                        string location, 
                        Image photo, 
                        string likes, 
                        string description)
    {
        PostData p = new PostData();
        p.ProfilePic = profilePic;
        p.FriendName = friendName;
        p.Location = location;
        p.Photo = photo;
        p.Likes = likes;
        p.Description = description;
        posts.Add(p);
        UpdateTimeStamp();
        
    }

    private void UpdateTimeStamp()
    {
        timeStamp++;
        if (timeStamp <= 0)
        {
            timeStamp = 1;
        }
    }

    public int GetTimeStamp()
    {
        return timeStamp;
    }

    public List<PostData> GetPostData()
    {
        return posts;
    }
}
