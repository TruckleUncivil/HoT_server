using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketServer
{
    class IncomingMessageParser
    {

        public static  string ParseIncomingMessage(NetworkMessage message)
        {
            string returnString = null;

            switch (message.Command)
            {
                case "RequestRoomLog":
               returnString =  NetworkCommandHandler.RequestRoomLog(message.ConcernedRoomName ,Int32.Parse(message.CommandArgs));

                    break;

                case "HandleCreateRankedGame":

                    returnString = NetworkCommandHandler.CreateRankedGame(message);

                    break;
                case "RequestRoomOfRank":

                    returnString = NetworkCommandHandler.ServeOpenRoomOfRank(message);
                    break;
                    
                case "PlayerJoinedRoom":
                    returnString = NetworkCommandHandler.PlayerJoinedRoom(message);


                    break;

                //case "SpawnUnit":

                //    returnString = NetworkCommandHandler.ServeSpawnUnitRequest(message);
                //    break;

                 default:
//Release code \/
                 //   returnString = "Function not implemented yet: " + message.Command;

                    //TEMP test code just to push through the messages

                    returnString = NetworkCommandHandler.ForceMessageThrough(message.ConcernedRoomName, message.Command,
                        message.CommandArgs);


                    break;

            }
            return returnString;
        }

     
    }


}
