using Hotel.Helpers;
using Hotel.Models.BusinessLogicLayer;
using Hotel.Models.EntityLayer;
using Hotel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Models.Services
{
    class ReservationActions
    {
        /* Business Logic Layer */
        private RoomsBLL roomBLL = new RoomsBLL();
        private FeaturesBLL featureBLL = new FeaturesBLL();
        private ExtraServicesBLL extraServicesBLL = new ExtraServicesBLL();
        private DiscountBLL discountBLL = new DiscountBLL();

        ReservationVM viewModel;

        public ReservationActions(ReservationVM vm,
                                RoomsBLL roomBLL,
                                FeaturesBLL featureBLL,
                                ExtraServicesBLL extraServicesBLL,
                                DiscountBLL discountBLL)
        {
            viewModel = vm;

            this.roomBLL = roomBLL;
            this.featureBLL = featureBLL;
            this.extraServicesBLL = extraServicesBLL;
            this.discountBLL = discountBLL;
        }

        public void Search(object param)
        {
            var context = new HotelEntities();
            var list = new List<Camera>();

            foreach(var camera in context.Camera)
            {
                bool nerezervata = true;
                foreach(var rezervare in camera.Rezervare)
                {
                    if (rezervare.StartDate >= viewModel.startDateTime && rezervare.EndDate <= viewModel.endDateTime)
                    {
                        nerezervata = false;
                        break;
                    }
                }
                if (nerezervata)
                {
                    var room = new Room() { Id = camera.Id_Camera, Price = camera.Price, Type = camera.Type };
                    viewModel.RoomList.Add(room);
                    list.Add(camera);
                }
            }

            foreach(var camera in list)
            {
                foreach(var oferta in camera.Oferta)
                {
                    viewModel.DiscountList.Add(new Discount()
                    {
                        Id = oferta.Id_Oferta,
                        Type = oferta.Type,
                        Value = oferta.Value,
                        StartDate = oferta.StartDate,
                        EndDate = oferta.EndDate
                    });
                }
            }

            foreach(var service in context.ServiciiSuplimentare)
            {
                viewModel.ExtraServicesList.Add(new ExtraService()
                {
                    Id = service.Id_ServiciiSuplimentare,
                    Price = service.Price,
                    Type = service.Type
                });
            }

            var allRes = context.Rezervare.Where(i => i.Id_Client == DataManager.Instance.CurrentUser.Id_Client);
            foreach (var res in allRes)
            {
                viewModel.ReservationList.Add(res);
            }
        }

        public void SelectRoom(object param)
        {
            var temp = param as Room;

            var r = new Room()
            {
                Id = temp.Id,
                Price = temp.Price,
                Type = temp.Type
            };
            DataManager.Instance.CurrentRooms.Add(r);
            viewModel.TotalInt += temp.Price;
            viewModel.RoomList.Remove(temp);
        }

        public void SelectService(object param)
        {
            var temp = param as ExtraService;

            var s = new ExtraService()
            {
                Id = temp.Id,
                Type = temp.Type,
                Price = temp.Price
            };
            DataManager.Instance.CurrentServices.Add(s);
            viewModel.TotalInt += temp.Price; 
            viewModel.ExtraServicesList.Remove(temp);
        }

        public void SelectDiscount(object param)
        {
            var temp = param as Discount;

            var d = new Discount()
            {
                Id = temp.Id,
                Type = temp.Type,
                Value = temp.Value,
                StartDate = temp.StartDate,
                EndDate = temp.EndDate
            };
            DataManager.Instance.CurrentDiscounts.Add(d);
            var cal = (double)(temp.Value / 100);
            cal = (cal * viewModel.TotalInt);
            viewModel.TotalInt = viewModel.TotalInt - (int)cal;
            viewModel.DiscountList.Remove(temp);
        }

        public void AddReservation(object param)
        {
            var r = new Rezervare()
            {
                StartDate = viewModel.startDateTime,
                EndDate = viewModel.endDateTime,
                State = "Activ"
            };
            viewModel.ReservationList.Add(r);

            var context = new HotelEntities();

            
            context.SaveChanges();
        }

        public void CancelReservation(object param)
        {
            var temp = param as Rezervare;

            viewModel.ReservationList.Remove(temp);
        }

        public void PayReservation(object param)
        {

            var temp = param as Rezervare;

            viewModel.ReservationList.Remove(temp);
        }
    }
}
