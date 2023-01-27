/*Una importante empresa del país requiere un sistema de nómina. Usted deberá desarrollar dicho sistema en modo consola, utilizando el lenguaje C#. 
Deberá entregar los códigos fuentes en un archivo txt. Dicha práctica es para realizar de manera individual.*/


/*El sistema deberá cumplir con los siguientes requerimientos:

Pedir por el teclado la cantidad de empleados a ingresar;
Pedir por el teclado y almacenar en una colección de datos los nombres, apellidos y salario bruto de los empleados;
Calcular las deducciones mediante el uso de funciones  (AFP, SS, ISR) para cada empleado y almacenadas en arreglos;
Mediante un procedimiento, deberá mostrar reporte similar a la imagen suministrada;
Utilizar acumuladores para los diferentes totales generales.*/

using System;

namespace ConsoleApp15
{
    internal class Program
    {
        //CLASE EMPLEADO (atributos y comportamientos)
        class Empleado
        {
            public string Nombre { get; set; }
            public decimal SalarioBruto { get; set; }
            public decimal ISR { get; set; }

            public Empleado(string nombre, decimal salarioBruto, decimal isr)
            {
                Nombre = nombre;

                SalarioBruto = salarioBruto;
                ISR = isr;
            }
        }

        static void Main(string[] args)
        {
            int CantEmpleados;
            int cont = 1; 
            Console.Write("Digita la cantidad de empleados: ");
            CantEmpleados = Convert.ToInt32(Console.ReadLine());

            Empleado[] empleados = new Empleado[CantEmpleados];
            decimal isr = 0;
            for (int i = 0; i < CantEmpleados; i++)
            {
                Console.Write($"Ingrese el nombre del empleado {cont}: ");
                string nombre = Console.ReadLine();
                
                Console.Write($"Ingrese el nombre del sueldo bruto {cont}: ");
                decimal salarioBruto = Convert.ToDecimal(Console.ReadLine());
                
                cont++;

                Empleado empleado = new Empleado(nombre, salarioBruto, isr);

                empleados[i] = empleado;
            }
            //SEGURO DISCAPACIDAD Y SUPERVIVENCIA AFP (2.87%)
            decimal AFP = 0.0287m;

            //SEGURO FAMILIAR DE SALUD SFS (3.04%)
            decimal SS = 0.0304m;
            
            //SUMATORIA
            decimal sumaSalarios = 0;
            decimal sumaAFP = 0;
            decimal sumaSS = 0;
            decimal sumaISR = 0;
            
            decimal isrCalculado = CalculoISR(empleados, AFP, SS);
            decimal acumuladorISR = 0;
            for (int i = 0; i < CantEmpleados; i++)
            {
                sumaSalarios += empleados[i].SalarioBruto;
                sumaAFP += empleados[i].SalarioBruto * AFP;
                sumaSS += empleados[i].SalarioBruto * SS;
                sumaISR += acumuladorISR;
            }

            MostrarTabla(empleados, CantEmpleados, sumaSalarios, sumaAFP, AFP, sumaSS, SS, sumaISR, isrCalculado, acumuladorISR);
        }

        static decimal CalculoISR(Empleado[] empleados, decimal AFP, decimal SS)
        {
            decimal acumuladorISR = 0;

            for (int j = 0; j < empleados.Length; j++)
            {
                decimal sueldoCotizableDGII = empleados[j].SalarioBruto;
                decimal sueldoCotizableAnual = sueldoCotizableDGII * 12;
                decimal ISR = 0;
                decimal escalaMinima = 416220m;
                decimal escalaMedia = 624329m;
                decimal escalaMaxima = 867123m;
                decimal excedente = 0;
                decimal multiPorcentaje;
                decimal valor1 = 0, valor2 = 31216m, valor3 = 79776m;
                decimal divMensual = 0;

                if (sueldoCotizableAnual <= escalaMinima && sueldoCotizableAnual >= 0)
                {
                    //EXCENTO
                    ISR = 0;
                }
                else if (sueldoCotizableAnual <= escalaMedia && sueldoCotizableAnual >= escalaMinima)
                {
                    excedente = sueldoCotizableAnual - escalaMinima;
                    multiPorcentaje = excedente * 0.15m;
                    divMensual = multiPorcentaje + valor1;
                    ISR = (divMensual) / 12;
                }
                else if (sueldoCotizableAnual <= escalaMaxima && sueldoCotizableAnual >= escalaMedia)
                {
                    excedente = sueldoCotizableAnual - escalaMedia;
                    multiPorcentaje = excedente * 0.20m;
                    divMensual = multiPorcentaje + valor2;
                    ISR = (divMensual) / 12;
                }
                else if (sueldoCotizableAnual >= escalaMaxima)
                {
                    excedente = sueldoCotizableAnual - escalaMedia;
                    multiPorcentaje = excedente * 0.25m;
                    divMensual = multiPorcentaje + valor3;
                    ISR = (divMensual) / 12;
                }

                // Asignar el ISR calculado al empleado
                empleados[j].ISR = ISR;

                acumuladorISR += ISR;
            }

            return acumuladorISR;
        }

        //METODO PARA MOSTRAR LA TABLA
        
        static void MostrarTabla(Empleado[] empleados, int CantEmpleados, decimal sumaSalarios, decimal sumaAFP, decimal AFP, decimal sumaSS, decimal SS, decimal sumaISR, decimal isrCalculado, decimal acumuladorISR)
        {

            int i = 0;
            int j = 0;

            Console.WriteLine("\n");
            Console.WriteLine("║ IDS - Ismael Porfirio Martínez Encarnación   ║  ASIGNATURA - LAB. DESARROLLO DE SOFTWARE 1   ║   ID: 1077546   ║    INTEC   ║");
            Console.WriteLine("\n");

            //EMPLADOS ARRAY
            
            if (CantEmpleados == 1)
            {
                decimal deducciones = empleados[i].SalarioBruto * AFP + empleados[i].SalarioBruto * SS + empleados[j].ISR;
                decimal deduccionRedondeado = Math.Round(deducciones, 2);
                decimal salarioNeto = empleados[i].SalarioBruto - empleados[i].SalarioBruto * AFP - empleados[i].SalarioBruto * SS - empleados[j].ISR;
                decimal salarioNetoRedondeado = Math.Round(salarioNeto, 2);

                string[,] matrizc = new string[7, 7]
                 {
                { "═════════════════", "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "════════════════"},
                 
                { "Nombre completo",  "  Sueldo Bruto  ",  "      AFP      ",  "       SFS      ", "       ISR      ", "  Deducciones  ", "  Salario Neto  ",},

                { "----------------", "----------------",  "---------------",  "---------------",  "---------------",  "---------------",  "---------------"},

                { empleados[i].Nombre, Convert.ToString(empleados[i].SalarioBruto), Convert.ToString(empleados[i].SalarioBruto*AFP), Convert.ToString(empleados[i].SalarioBruto*SS), Convert.ToString(empleados[j].ISR), Convert.ToString(deduccionRedondeado), Convert.ToString(salarioNetoRedondeado)},

                { "----------------", "----------------",  "---------------",  "---------------",  "---------------",  "---------------",  "---------------" },

                { "Total general", Convert.ToString(sumaSalarios), Convert.ToString(sumaAFP), Convert.ToString(sumaSS), Convert.ToString( empleados[j].ISR), Convert.ToString(deduccionRedondeado), Convert.ToString(salarioNetoRedondeado)},
                 { "═════════════════", "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "════════════════"},

                 };

                bidimencional<string>(matrizc, 16);
            }

            //recuerda arreglar la siguiente tabla

            if (CantEmpleados == 2)
            {
                decimal deducciones = empleados[i].SalarioBruto * AFP + empleados[i].SalarioBruto * SS + empleados[j].ISR;
                decimal deduccionRedondeado = Math.Round(deducciones, 2);
                decimal salarioNeto = empleados[i].SalarioBruto - empleados[i].SalarioBruto * AFP - empleados[i].SalarioBruto * SS - empleados[j].ISR;
                decimal salarioNetoRedondeado = Math.Round(salarioNeto, 2);

                decimal deducciones1 = empleados[i+1].SalarioBruto * AFP + empleados[i+1].SalarioBruto * SS + empleados[j+1].ISR;
                decimal deduccionRedondeado1 = Math.Round(deducciones1, 2);
                decimal salarioNeto1 = empleados[i+1].SalarioBruto - empleados[i+1].SalarioBruto * AFP - empleados[i].SalarioBruto * SS - empleados[j+1].ISR;
                decimal salarioNetoRedondeado1 = Math.Round(salarioNeto1, 2);


                string[,] matrizc = new string[8, 7]
                 {
                { "═════════════════", "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "════════════════"},
                
                
                { "Nombre completo",  "  Sueldo Bruto  ",  "      AFP      ",  "       SFS      ", "       ISR      ", "  Deducciones  ", "  Salario Neto  ",},

                { "----------------", "----------------",  "---------------",  "---------------",  "---------------",  "---------------",  "---------------"},
                { empleados[i].Nombre, Convert.ToString(empleados[i].SalarioBruto), Convert.ToString(empleados[i].SalarioBruto*AFP), Convert.ToString(empleados[i].SalarioBruto*SS), Convert.ToString(empleados[j].ISR), Convert.ToString(deduccionRedondeado), Convert.ToString(salarioNetoRedondeado)},

                { empleados[i+1].Nombre, Convert.ToString(empleados[i+1].SalarioBruto), Convert.ToString(empleados[i+1].SalarioBruto*AFP), Convert.ToString(empleados[i+1].SalarioBruto*SS), Convert.ToString(empleados[j+1].ISR), Convert.ToString(deduccionRedondeado1), Convert.ToString(salarioNetoRedondeado1)},

                { "----------------", "----------------",  "---------------",  "---------------",  "---------------",  "---------------",  "---------------" },

                { "Total general", Convert.ToString(sumaSalarios), Convert.ToString(sumaAFP), Convert.ToString(sumaSS), Convert.ToString( empleados[j].ISR + empleados[j+1].ISR), Convert.ToString(deduccionRedondeado + deduccionRedondeado1), Convert.ToString(salarioNetoRedondeado + salarioNetoRedondeado1)},
                
                { "═════════════════", "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════", "═══════════════"},

                 };
                

                bidimencional<string>(matrizc, 16);
            }

            if (CantEmpleados == 3)
            {
                decimal deducciones = empleados[i].SalarioBruto * AFP + empleados[i].SalarioBruto * SS + empleados[j].ISR;
                decimal deduccionRedondeado = Math.Round(deducciones, 2);
                decimal salarioNeto = empleados[i].SalarioBruto - empleados[i].SalarioBruto * AFP - empleados[i].SalarioBruto * SS - empleados[j].ISR;
                decimal salarioNetoRedondeado = Math.Round(salarioNeto, 2);

                decimal deducciones1 = empleados[i + 1].SalarioBruto * AFP + empleados[i + 1].SalarioBruto * SS + empleados[j + 1].ISR;
                decimal deduccionRedondeado1 = Math.Round(deducciones1, 2);
                decimal salarioNeto1 = empleados[i + 1].SalarioBruto - empleados[i + 1].SalarioBruto * AFP - empleados[i+1].SalarioBruto * SS - empleados[j + 1].ISR;
                decimal salarioNetoRedondeado1 = Math.Round(salarioNeto1, 2);

                decimal deducciones2 = empleados[i + 2].SalarioBruto * AFP + empleados[i + 2].SalarioBruto * SS + empleados[j + 2].ISR;
                decimal deduccionRedondeado2 = Math.Round(deducciones2, 2);
                decimal salarioNeto2 = empleados[i + 2].SalarioBruto - empleados[i + 2].SalarioBruto * AFP - empleados[i+2].SalarioBruto * SS - empleados[j + 2].ISR;
                decimal salarioNetoRedondeado2 = Math.Round(salarioNeto2, 2);

                string[,] matrizc = new string[9, 7]
                 {
                 { "═════════════════", "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "════════════════"},
                { "Nombre completo",  "  Sueldo Bruto  ",  "      AFP      ",  "       SFS      ", "       ISR      ", "  Deducciones  ", "  Salario Neto  ",},

                { "----------------", "----------------",  "---------------",  "---------------",  "---------------",  "---------------",  "---------------"},
                { empleados[i].Nombre, Convert.ToString(empleados[i].SalarioBruto), Convert.ToString(empleados[i].SalarioBruto*AFP), Convert.ToString(empleados[i].SalarioBruto*SS), Convert.ToString(empleados[j].ISR), Convert.ToString(deduccionRedondeado), Convert.ToString(salarioNetoRedondeado)},

                { empleados[i+1].Nombre, Convert.ToString(empleados[i+1].SalarioBruto), Convert.ToString(empleados[i+1].SalarioBruto*AFP), Convert.ToString(empleados[i+1].SalarioBruto*SS), Convert.ToString(empleados[j+1].ISR), Convert.ToString(deduccionRedondeado1), Convert.ToString(salarioNetoRedondeado1)},
               
                { empleados[i+2].Nombre, Convert.ToString(empleados[i+2].SalarioBruto), Convert.ToString(empleados[i+2].SalarioBruto*AFP), Convert.ToString(empleados[i+2].SalarioBruto*SS), Convert.ToString(empleados[j+2].ISR), Convert.ToString(deduccionRedondeado2), Convert.ToString(salarioNetoRedondeado2)},

                { "----------------", "----------------",  "---------------",  "---------------",  "---------------",  "---------------",  "---------------" },

                { "Total general", Convert.ToString(sumaSalarios), Convert.ToString(sumaAFP), Convert.ToString(sumaSS), Convert.ToString( empleados[j].ISR + empleados[j+1].ISR + empleados[j+2].ISR), Convert.ToString(deduccionRedondeado + deduccionRedondeado1 + deduccionRedondeado2), Convert.ToString(salarioNetoRedondeado + salarioNetoRedondeado1 + salarioNetoRedondeado2)},
                 { "═════════════════", "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "════════════════"}

                 };

                bidimencional<string>(matrizc, 16);
            }

            if (CantEmpleados == 4)
            {
                decimal deducciones = empleados[i].SalarioBruto * AFP + empleados[i].SalarioBruto * SS + empleados[j].ISR;
                decimal deduccionRedondeado = Math.Round(deducciones, 2);
                decimal salarioNeto = empleados[i].SalarioBruto - empleados[i].SalarioBruto * AFP - empleados[i].SalarioBruto * SS - empleados[j].ISR;
                decimal salarioNetoRedondeado = Math.Round(salarioNeto, 2);

                decimal deducciones1 = empleados[i + 1].SalarioBruto * AFP + empleados[i + 1].SalarioBruto * SS + empleados[j + 1].ISR;
                decimal deduccionRedondeado1 = Math.Round(deducciones1, 2);
                decimal salarioNeto1 = empleados[i + 1].SalarioBruto - empleados[i + 1].SalarioBruto * AFP - empleados[i + 1].SalarioBruto * SS - empleados[j + 1].ISR;
                decimal salarioNetoRedondeado1 = Math.Round(salarioNeto1, 2);

                decimal deducciones2 = empleados[i + 2].SalarioBruto * AFP + empleados[i + 2].SalarioBruto * SS + empleados[j + 2].ISR;
                decimal deduccionRedondeado2 = Math.Round(deducciones2, 2);
                decimal salarioNeto2 = empleados[i + 2].SalarioBruto - empleados[i + 2].SalarioBruto * AFP - empleados[i + 2].SalarioBruto * SS - empleados[j + 2].ISR;
                decimal salarioNetoRedondeado2 = Math.Round(salarioNeto2, 2);
                
                decimal deducciones3 = empleados[i + 3].SalarioBruto * AFP + empleados[i + 3].SalarioBruto * SS + empleados[j + 3].ISR;
                decimal deduccionRedondeado3 = Math.Round(deducciones3, 2);
                decimal salarioNeto3 = empleados[i + 3].SalarioBruto - empleados[i + 3].SalarioBruto * AFP - empleados[i + 3].SalarioBruto * SS - empleados[j + 3].ISR;
                decimal salarioNetoRedondeado3 = Math.Round(salarioNeto3, 2);

                string[,] matrizc = new string[10, 7]
                 {
                 { "═════════════════", "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "════════════════"},
                { "Nombre completo",  "  Sueldo Bruto  ",  "      AFP      ",  "       SFS      ", "       ISR      ", "  Deducciones  ", "  Salario Neto  ",},

                { "----------------", "----------------",  "---------------",  "---------------",  "---------------",  "---------------",  "---------------"},
                { empleados[i].Nombre, Convert.ToString(empleados[i].SalarioBruto), Convert.ToString(empleados[i].SalarioBruto*AFP), Convert.ToString(empleados[i].SalarioBruto*SS), Convert.ToString(empleados[j].ISR), Convert.ToString(deduccionRedondeado), Convert.ToString(salarioNetoRedondeado)},

                { empleados[i+1].Nombre, Convert.ToString(empleados[i+1].SalarioBruto), Convert.ToString(empleados[i+1].SalarioBruto*AFP), Convert.ToString(empleados[i+1].SalarioBruto*SS), Convert.ToString(empleados[j+1].ISR), Convert.ToString(deduccionRedondeado1), Convert.ToString(salarioNetoRedondeado1)},

                { empleados[i+2].Nombre, Convert.ToString(empleados[i+2].SalarioBruto), Convert.ToString(empleados[i+2].SalarioBruto*AFP), Convert.ToString(empleados[i+2].SalarioBruto*SS), Convert.ToString(empleados[j+2].ISR), Convert.ToString(deduccionRedondeado2), Convert.ToString(salarioNetoRedondeado2)},
                
                { empleados[i+3].Nombre, Convert.ToString(empleados[i+3].SalarioBruto), Convert.ToString(empleados[i+3].SalarioBruto*AFP), Convert.ToString(empleados[i+3].SalarioBruto*SS), Convert.ToString(empleados[j+3].ISR), Convert.ToString(deduccionRedondeado3), Convert.ToString(salarioNetoRedondeado3)},

                { "----------------", "----------------",  "---------------",  "---------------",  "---------------",  "---------------",  "---------------" },

                { "Total general", Convert.ToString(sumaSalarios), Convert.ToString(sumaAFP), Convert.ToString(sumaSS), Convert.ToString( empleados[j].ISR + empleados[j+1].ISR + empleados[j+2].ISR + empleados[j+3].ISR), Convert.ToString(deduccionRedondeado + deduccionRedondeado1 + deduccionRedondeado2 + deduccionRedondeado3), Convert.ToString(salarioNetoRedondeado + salarioNetoRedondeado1 + salarioNetoRedondeado2 + salarioNetoRedondeado3)},
                 { "═════════════════", "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "════════════════"}

                 };

                bidimencional<string>(matrizc, 16);
            }

            if (CantEmpleados == 5)
            {
                decimal deducciones = empleados[i].SalarioBruto * AFP + empleados[i].SalarioBruto * SS + empleados[j].ISR;
                decimal deduccionRedondeado = Math.Round(deducciones, 2);
                decimal salarioNeto = empleados[i].SalarioBruto - empleados[i].SalarioBruto * AFP - empleados[i].SalarioBruto * SS - empleados[j].ISR;
                decimal salarioNetoRedondeado = Math.Round(salarioNeto, 2);

                decimal deducciones1 = empleados[i + 1].SalarioBruto * AFP + empleados[i + 1].SalarioBruto * SS + empleados[j + 1].ISR;
                decimal deduccionRedondeado1 = Math.Round(deducciones1, 2);
                decimal salarioNeto1 = empleados[i + 1].SalarioBruto - empleados[i + 1].SalarioBruto * AFP - empleados[i + 1].SalarioBruto * SS - empleados[j + 1].ISR;
                decimal salarioNetoRedondeado1 = Math.Round(salarioNeto1, 2);

                decimal deducciones2 = empleados[i + 2].SalarioBruto * AFP + empleados[i + 2].SalarioBruto * SS + empleados[j + 2].ISR;
                decimal deduccionRedondeado2 = Math.Round(deducciones2, 2);
                decimal salarioNeto2 = empleados[i + 2].SalarioBruto - empleados[i + 2].SalarioBruto * AFP - empleados[i + 2].SalarioBruto * SS - empleados[j + 2].ISR;
                decimal salarioNetoRedondeado2 = Math.Round(salarioNeto2, 2);

                decimal deducciones3 = empleados[i + 3].SalarioBruto * AFP + empleados[i + 3].SalarioBruto * SS + empleados[j + 3].ISR;
                decimal deduccionRedondeado3 = Math.Round(deducciones3, 2);
                decimal salarioNeto3 = empleados[i + 3].SalarioBruto - empleados[i + 3].SalarioBruto * AFP - empleados[i + 3].SalarioBruto * SS - empleados[j + 3].ISR;
                decimal salarioNetoRedondeado3 = Math.Round(salarioNeto3, 2);

                decimal deducciones4 = empleados[i + 4].SalarioBruto * AFP + empleados[i + 4].SalarioBruto * SS + empleados[j + 4].ISR;
                decimal deduccionRedondeado4 = Math.Round(deducciones4, 2);
                decimal salarioNeto4 = empleados[i + 4].SalarioBruto - empleados[i + 4].SalarioBruto * AFP - empleados[i + 4].SalarioBruto * SS - empleados[j + 4].ISR;
                decimal salarioNetoRedondeado4 = Math.Round(salarioNeto4, 2);

                string[,] matrizc = new string[11, 7]
                 {
                 { "═════════════════", "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "════════════════"},
                { "Nombre completo",  "  Sueldo Bruto  ",  "      AFP      ",  "       SFS      ", "       ISR      ", "  Deducciones  ", "  Salario Neto  ",},

                { "----------------", "----------------",  "---------------",  "---------------",  "---------------",  "---------------",  "---------------"},
                { empleados[i].Nombre, Convert.ToString(empleados[i].SalarioBruto), Convert.ToString(empleados[i].SalarioBruto*AFP), Convert.ToString(empleados[i].SalarioBruto*SS), Convert.ToString(empleados[j].ISR), Convert.ToString(deduccionRedondeado), Convert.ToString(salarioNetoRedondeado)},

                { empleados[i+1].Nombre, Convert.ToString(empleados[i+1].SalarioBruto), Convert.ToString(empleados[i+1].SalarioBruto*AFP), Convert.ToString(empleados[i+1].SalarioBruto*SS), Convert.ToString(empleados[j+1].ISR), Convert.ToString(deduccionRedondeado1), Convert.ToString(salarioNetoRedondeado1)},

                { empleados[i+2].Nombre, Convert.ToString(empleados[i+2].SalarioBruto), Convert.ToString(empleados[i+2].SalarioBruto*AFP), Convert.ToString(empleados[i+2].SalarioBruto*SS), Convert.ToString(empleados[j+2].ISR), Convert.ToString(deduccionRedondeado2), Convert.ToString(salarioNetoRedondeado2)},

                { empleados[i+3].Nombre, Convert.ToString(empleados[i+3].SalarioBruto), Convert.ToString(empleados[i+3].SalarioBruto*AFP), Convert.ToString(empleados[i+3].SalarioBruto*SS), Convert.ToString(empleados[j+3].ISR), Convert.ToString(deduccionRedondeado3), Convert.ToString(salarioNetoRedondeado3)},
                
                { empleados[i+4].Nombre, Convert.ToString(empleados[i+4].SalarioBruto), Convert.ToString(empleados[i+4].SalarioBruto*AFP), Convert.ToString(empleados[i+4].SalarioBruto*SS), Convert.ToString(empleados[j+4].ISR), Convert.ToString(deduccionRedondeado4), Convert.ToString(salarioNetoRedondeado4)},

                { "----------------", "----------------",  "---------------",  "---------------",  "---------------",  "---------------",  "---------------" },

                { "Total general", Convert.ToString(sumaSalarios), Convert.ToString(sumaAFP), Convert.ToString(sumaSS), Convert.ToString( empleados[j].ISR + empleados[j+1].ISR + empleados[j+2].ISR + empleados[j+3].ISR), Convert.ToString(deduccionRedondeado + deduccionRedondeado1 + deduccionRedondeado2 + deduccionRedondeado3 + deduccionRedondeado4), Convert.ToString(salarioNetoRedondeado + salarioNetoRedondeado1 + salarioNetoRedondeado2 + salarioNetoRedondeado3 + salarioNetoRedondeado4)},
                 { "═════════════════", "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "═══════════════",  "════════════════"}

                 };

                bidimencional<string>(matrizc, 16);
            }

        }

        //MATRIZ BIDIMENCIONAL 

        public static void bidimencional<T>(T[,] matriz, int maxLength) where T : IConvertible
        {
            var filas = matriz.GetLength(0);
            var columnas = matriz.GetLength(1);
            var sb = "";
            var tmpfila = new T[matriz.GetLength(1)];
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    string value = Convert.ToString(matriz[i, j]);
                    if (value.Length > maxLength)
                    {
                        value = value.Substring(0, maxLength);
                    }
                    else if (value.Length < maxLength)
                    {
                        value = value.PadRight(maxLength);
                    }
                    tmpfila[j] = (T)Convert.ChangeType(value, typeof(T));
                }
                sb += "| " + string.Join(" | ", tmpfila) + " |" + Environment.NewLine;
            }
            Console.WriteLine(sb);
        }
    }
}
