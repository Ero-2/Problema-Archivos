using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Problema_Archivos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string filePath = "registro_ventas.txt"; // Ruta del archivo de registro de ventas

            try
            {
                Dictionary<DateTime, decimal> ventasPorDia = CalcularVentasPorDia(filePath);

                Console.WriteLine("Total de ventas por día:");

                foreach (var kvp in ventasPorDia)
                {
                    Console.WriteLine($"{kvp.Key.ToShortDateString()}: {kvp.Value:C}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ocurrió un error al procesar el archivo: " + ex.Message);
            }
        }
            static Dictionary<DateTime, decimal> CalcularVentasPorDia(string filePath)
            {
                Dictionary<DateTime, decimal> ventasPorDia = new Dictionary<DateTime, decimal>();

                // Verifica si el archivo existe
                if (File.Exists(filePath))
                {
                    // Lee todas las líneas del archivo
                    string[] lineas = File.ReadAllLines(filePath);

                    foreach (string linea in lineas)
                    {
                        // Divide la línea en fecha y monto de venta utilizando el carácter '|'
                        string[] partes = linea.Split('|');

                        if (partes.Length == 2)
                        {
                            // Parsea la fecha y el monto de venta
                            if (DateTime.TryParse(partes[0], out DateTime fecha) && decimal.TryParse(partes[1], out decimal montoVenta))
                            {
                                // Agrega el monto de venta a la suma total para el día correspondiente
                                if (ventasPorDia.ContainsKey(fecha.Date))
                                {
                                    ventasPorDia[fecha.Date] += montoVenta;
                                }
                                else
                                {
                                    ventasPorDia.Add(fecha.Date, montoVenta);
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Error al analizar la línea: {linea}. Se omitirá.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Error al analizar la línea: {linea}. Se omitirá.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("El archivo no existe.");
                }

                return ventasPorDia;
            }


        

    }
}
