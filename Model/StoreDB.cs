using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using MVVMInmobiliaria;
using MVVMInmobiliaria.ViewModels;

namespace MVVMInmobiliaria.Model
{
    public class StoreDB
    {
        public bool hasError = false;
        public string errorMessage;
        
        public MyObservableCollection<Inmueble> GetInmuebles()
        {
            hasError = false;
            MyObservableCollection<Inmueble> inmueble = new MyObservableCollection<Inmueble>();
            try
            {
                LinqDataContext dc = new LinqDataContext();
                var query = from q in dc.LinqProducts
                    select new SqlInmueble{
                        InmuebleId = q.ProductID, Direccion = q.ModelNumber,
                        Vendedor=q.ModelName, Precio = (decimal)q.UnitCost,
                        Descripcion = q.Description, Categoria = q.LinqCategory.CategoryName
                    };
                foreach (SqlInmueble sp in query)
                    inmueble.Add(sp.SqlProduct2Product());
            }
            catch(Exception ex)
            {
                errorMessage = "GetInmuebles() error, " + ex.Message;
                hasError = true;
            }
            return inmueble;
        }

        public bool UpdateInmueble(Inmueble mostrarInmueble)
        {
            try
            {
                SqlInmueble inmueble = new SqlInmueble(mostrarInmueble);
                LinqDataContext dc = new LinqDataContext();
                dc.UpdateInmueble(inmueble.InmuebleId, inmueble.Categoria, inmueble.Direccion, inmueble.Vendedor, inmueble.Precio, inmueble.Descripcion);
            }
            catch (Exception ex)
            {
                errorMessage = "Update error, " + ex.Message;
                hasError = true;
            }
            return (!hasError);
        }

        public bool DeleteInmueble(int inmuebleId)
        {
            hasError = false;
            try
            {
                LinqDataContext dc = new LinqDataContext();
                dc.DeleteInmueble(inmuebleId);
            }
            catch (Exception ex)
            {
                errorMessage = "Delete error, " + ex.Message;
                hasError = true;
            }
            return !hasError;
        }

        public bool AddInmueble(Inmueble mostrarInmueble)
        {
            hasError = false;
            try
            {
                SqlInmueble inmueble = new SqlInmueble(mostrarInmueble);
                LinqDataContext dc = new LinqDataContext();
                int? nuevoInmuebleId = 0;
                dc.AddInmueble(inmueble.Categoria, inmueble.Direccion, inmueble.Vendedor, inmueble.Precio, inmueble.Descripcion, ref nuevoInmuebleId);
                inmueble.InmuebleId = (int)nuevoInmuebleId;
                mostrarInmueble.ProductAdded2DB(inmueble);
            }
            catch (Exception ex)
            {
                errorMessage = "Add error, " + ex.Message;
                hasError = true;
            }
            return !hasError;
        }
    }
}
