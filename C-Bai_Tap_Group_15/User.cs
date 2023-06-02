using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    internal class User
    {
        public string  IdPeople { set; get; }
        public string Name { set; get; }
        public string Contryside { set; get; }
        public DateTime ? BirthDay {set; get; }
        public string VehicleRegistrationPlace { set; get; }
        public string  Phone { set; get; }
        public string  Email { set; get; }
        public string  Gender { set; get; }
        public string Coddingregion { set; get; }
        public string emailregirts { set; get; }
        public User() {
        }
        public User(string Id,string name ,string contryside, DateTime birthday,string vehicleRegistrationPlace,string Phone,string Email,string Gender,string Coddingregion ,string emailregirts)
        {
            this.IdPeople = Id;
            this.Name = name;
            this.Contryside = contryside;
            this.BirthDay = birthday;
            this.VehicleRegistrationPlace = vehicleRegistrationPlace;
            this.Phone = Phone;
            this.Email = Email;
            this.Gender = Gender;
            this.Coddingregion = Coddingregion;
            this.emailregirts = emailregirts;
        }
    }
}
