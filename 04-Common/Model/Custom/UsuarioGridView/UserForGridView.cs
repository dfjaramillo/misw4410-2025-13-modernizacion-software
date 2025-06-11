using System;
using System.Collections.Generic;
using Model.Auth;

namespace Model.Custom.UsuarioGridView
{
    public class UserForGridView
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public Int64 Identification { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public Int64 Area { get; set; }
        public string NombreArea { get; set; }
        public Int64 Cargo { get; set; }
        public string NombreCargo { get; set; }
        public List<string> Roles { get; set; }
        public bool Enable { get; set; }

        public UserForGridView()
        {
        }

        public UserForGridView(ApplicationUser model)
        {
            Id = model.Id;
            Identification = model.Identification;
            Name = model.Name;
            LastName = model.LastName;
            Email = model.Email;
            Area = model.Area;
            Cargo = model.Cargo;
        }
    }
}
