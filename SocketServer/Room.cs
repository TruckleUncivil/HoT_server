using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
 public   class Room
    {
        
       public string RoomName = "";
      public int Rank = 0 ;
      public int  MaxPlayerCount =  2;

     public Player MasterClient;

     public List<Player> Players = new List<Player>();

     public List<string> Log = new List<string>();

     public Map RoomMap;

     public List<Team> Teams = new List<Team>(); 


     public bool WaitingForPlayers()
     {
         if (Players.Count >= MaxPlayerCount)
         {
             return false;
         }
         else
         {
             return true;
         }
     }
     public void AddPlayerToRoom(Player player)
     {
         if (WaitingForPlayers())
         {
             Log.Add("RegisterPlayer:" + player.Name);
             Players.Add(player);

             Team team = new Team();
             team.Owner = player.Name;
             Teams.Add(team);

             if (Players.Count == MaxPlayerCount)
             {
                 Log.Add("LoadMap:0");
             }
                 
                 
         }
         else
         {
             //not waiting for player, handle that somehow
         }

     }

     public void CreateAgent(NetworkMessage message)
     {
         
     }
    }
}
