using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
  static   class NetworkCommandHandler
    {

      public static string RequestRoomLog(string roomName, int index)
      {
          string returnString = "";

          for (int i = index; i < AsynchronousSocketListener.FindRoomByName(roomName).Log.Count; i++)
          {
              returnString = returnString + AsynchronousSocketListener.FindRoomByName(roomName).Log[i] + "*";
          }

          return returnString;
      }

      public  static string ForceMessageThrough(string concernedRoom, string command, string commandArgs)
      {

          AsynchronousSocketListener.FindRoomByName(concernedRoom).Log.Add(command + ":" +commandArgs);
          string returnString = "success";

          Console.WriteLine("Function not yet implented, had to force through: " + command + ":" + commandArgs);
          return returnString;
      }


      public static string ServeOpenRoomOfRank(NetworkMessage message)
      {

          int rank = Int32.Parse(message.CommandArgs);
          List<Room> returnList = new List<Room>();

          foreach (Room room in AsynchronousSocketListener.Rooms)
          {
              if (room.WaitingForPlayers() && room.Rank == rank)
              {
                  returnList.Add(room);
              }
          }

          string returnString = "";

          if (returnList.Count() != 0)
          {
              returnString = returnList[0].RoomName;
          }

          return returnString;
      }

      public static string CreateRankedGame(NetworkMessage message)
      {
          string[] args = message.CommandArgs.Split(">".ToCharArray());


    Room room= AsynchronousSocketListener.MakeARoom(args[1], message.SendingPlayerName);

          room.Rank = Int32.Parse(args[0]);


          string returnString = "success";
     
          return returnString;
      }


      public static string PlayerJoinedRoom(NetworkMessage message)
      {
          string returnString = "success";

          if (AsynchronousSocketListener.FindRoomByName(message.CommandArgs).WaitingForPlayers())
          {
              Player player = new Player();
              player.Name = message.SendingPlayerName;

              AsynchronousSocketListener.FindRoomByName(message.CommandArgs).AddPlayerToRoom(player);

              Console.WriteLine("Server:" + player.Name + " just joined room " + message.CommandArgs);

          }
          else
          {
              returnString = "nope";
          }
          return returnString;

      }

      public  static string ServeSpawnUnitRequest(NetworkMessage message)
      {
           string returnString = "success";


          message.GetConcernedRoom().CreateAgent(message);

          return returnString;

      }
    }



}
