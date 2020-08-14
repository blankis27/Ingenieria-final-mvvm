using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MVVMInmobiliaria.ViewModels
{
    public class Inmueble
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private int _productId;
        public int _ProductId { get { return _productId; } }

        private string direccion;
        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; OnPropertyChanged(new PropertyChangedEventArgs("Direccion"));
                }
        }

        private string vendedor;
        public string Vendedor
        {
            get { return vendedor; }
            set { vendedor = value; OnPropertyChanged(new PropertyChangedEventArgs("Vendedor")); }
        }

        private string precio;
        public string Precio
        {
            get { return precio; }
            set { precio = value; OnPropertyChanged(new PropertyChangedEventArgs("Precio")); }
        }

        private string descripcion;
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; OnPropertyChanged(new PropertyChangedEventArgs("Descripcion")); }
        }

        private string categoria;
        public string Categoria
        {
            get { return categoria; }
            set { categoria = value; OnPropertyChanged(new PropertyChangedEventArgs("Categoria")); }
        }

        // STRING PARA MOSTRAR EN PANTALLA
        public string InmuebleCompleto
        {
            get {return Direccion + "\t|\t" + Vendedor + "\t|\t" + Precio + "\t|\t" + Categoria; }
        }

        public Inmueble()
        {
        }

        public Inmueble(int productId, string direccion, string vendedor,
                       string precio, string descripcion, string categoria)
        {
            this._productId = productId;
            Direccion = direccion;
            Vendedor = vendedor;
            Precio = precio;
            Descripcion = descripcion;
            Categoria = categoria;
        }

        public void CopyProduct(Inmueble inmueble)
        {
            this._productId = inmueble._ProductId;
            this.Direccion = inmueble.Direccion;
            this.Vendedor = inmueble.Vendedor;
            this.Precio = inmueble.Precio;
            this.Categoria = inmueble.Categoria;
            this.Descripcion = inmueble.Descripcion;
        }

        public void ProductAdded2DB(SqlInmueble sqlInmueble)
        {
            this._productId = sqlInmueble.InmuebleId;
        }


    }

    //Clase para comounicarse con SQL
    public class SqlInmueble
    {
        public int InmuebleId { get; set; }
        public string Direccion {get; set;}
        public string Vendedor {get; set;}
        public decimal Precio {get; set;}
        public string Descripcion {get; set;}
        public string Categoria {get; set;}

        public SqlInmueble() { }

        public SqlInmueble(int inmuebleId, string direccion, string vendedor,
                       decimal precio, string descripcion, string categoria)
        {
            InmuebleId = inmuebleId;
            Direccion = direccion;
            Vendedor = vendedor;
            Precio = precio;
            Descripcion = descripcion;
            Categoria = categoria;
        }

        public SqlInmueble(Inmueble inmueble)
        {
            InmuebleId = inmueble._ProductId;
            Direccion = inmueble.Direccion;
            Vendedor = inmueble.Vendedor;
            Precio = Convert.ToDecimal(inmueble.Precio);
            Descripcion = inmueble.Descripcion;
            Categoria = inmueble.Categoria;
        }

        public Inmueble SqlProduct2Product()
        {
            string precio = Precio.ToString();
            return new Inmueble(InmuebleId, Direccion, Vendedor, precio, Descripcion, Categoria);
        }
    }

}
