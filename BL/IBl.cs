using IBL.BO;

namespace BL
{
    public interface IBL
    {
        int AddDrone();
        void AddStation(string stationName, int positions);
        void AssignmentParcelToDrone(int parcelId, int station);
        BaseStation GetStation(int requestedId);
        void PickedupParcel(int parcelid);
        void RelizeDroneFromeRecharg(int droneId);
        void SendDroneToRecharge(int droneId, int station);
    }
}