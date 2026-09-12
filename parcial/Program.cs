using System;
using System.Collections.Generic;

class Program
{
	static void Main()
	{
		List<string> nombres = new List<string>();
		List<decimal> precios = new List<decimal>();
		List<int> stocks = new List<int>();
		List<int> unidadesVendidas = new List<int>();

		int totalVentas = 0;
		decimal totalCaja = 0m;
		int opcion;

		do
		{
			ImprimirMenu();
			opcion = LeerEntero("Seleccione una opción (1-5): ", 1, 5);

			try
			{
				switch (opcion)
				{
					case 1:
						RegistrarProducto(nombres, precios, stocks, unidadesVendidas);
						break;
					case 2:
						MostrarInventario(nombres, precios, stocks);
						break;
					case 3:
						RegistrarVenta(nombres, precios, stocks, unidadesVendidas, ref totalVentas, ref totalCaja);
						break;
					case 4:
						MostrarReporte(nombres, unidadesVendidas, totalVentas, totalCaja);
						break;
					case 5:
						Console.WriteLine("\nGracias por usar el sistema. ¡Hasta pronto!");
						break;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"[ERROR] No fue posible completar la operación: {ex.Message}");
			}

			if (opcion != 5)
			{
				Console.WriteLine("\nPresione ENTER para continuar...");
				Console.ReadLine();
			}
		} while (opcion != 5);
	}

	static int LeerEntero(string mensaje, int min, int max)
	{
		int valor;
		bool entradaValida;

		do
		{
			Console.Write(mensaje);
			entradaValida = int.TryParse(Console.ReadLine(), out valor) && valor >= min && valor <= max;

			if (!entradaValida)
			{
				Console.WriteLine($"[ERROR] Ingrese un número entero entre {min} y {max}.");
			}
		} while (!entradaValida);

		return valor;
	}

	static decimal LeerDecimal(string mensaje, decimal min)
	{
		decimal valor;
		bool entradaValida;

		do
		{
			Console.Write(mensaje);
			entradaValida = decimal.TryParse(Console.ReadLine(), out valor) && valor >= min;

			if (!entradaValida)
			{
				Console.WriteLine($"[ERROR] Ingrese un número decimal mayor o igual a {min:0.00}.");
			}
		} while (!entradaValida);

		return valor;
	}

	static decimal CalcularFactura(decimal precio, int cantidad, bool tieneDescuento, out decimal montoIva, out decimal montoDescuento)
	{
		decimal subtotal = precio * cantidad;
		montoDescuento = tieneDescuento ? subtotal * 0.10m : 0m;
		montoIva = (subtotal - montoDescuento) * 0.19m;
		return subtotal - montoDescuento + montoIva;
	}

	static void ImprimirEncabezado(string titulo)
	{
		Console.WriteLine("\n====================================================");
		Console.WriteLine($"                  {titulo}");
		Console.WriteLine("====================================================");
	}

	static void ImprimirMenu()
	{
		ImprimirEncabezado("SISTEMA GESTOR DE VENTAS E INVENTARIO (MINI-POS)");
		Console.WriteLine("1. Registrar nuevo producto en inventario");
		Console.WriteLine("2. Consultar inventario completo");
		Console.WriteLine("3. Registrar una venta");
		Console.WriteLine("4. Ver reporte de caja y estadísticas diarias");
		Console.WriteLine("5. Salir");
		Console.WriteLine("====================================================");
	}

	static void RegistrarProducto(List<string> nombres, List<decimal> precios, List<int> stocks, List<int> unidadesVendidas)
	{
		ImprimirEncabezado("REGISTRAR PRODUCTO");
		string nombre;

		do
		{
			Console.Write("Nombre del producto: ");
			nombre = (Console.ReadLine() ?? string.Empty).Trim();

			if (nombre == string.Empty)
			{
				Console.WriteLine("[ERROR] El nombre no puede estar vacío.");
			}
		} while (nombre == string.Empty);

		for (int i = 0; i < nombres.Count; i++)
		{
			if (nombres[i].ToLower() == nombre.ToLower())
			{
				Console.WriteLine("[ERROR] Ya existe un producto con ese nombre.");
				return;
			}
		}

		decimal precio = LeerDecimal("Precio unitario ($): ", 0.01m);
		int stock = LeerEntero("Stock inicial: ", 0, int.MaxValue);

		nombres.Add(nombre);
		precios.Add(precio);
		stocks.Add(stock);
		unidadesVendidas.Add(0);
		Console.WriteLine("[OK] Producto registrado correctamente.");
	}

	static void MostrarInventario(List<string> nombres, List<decimal> precios, List<int> stocks)
	{
		ImprimirEncabezado("INVENTARIO COMPLETO");

		if (nombres.Count == 0)
		{
			Console.WriteLine("No hay productos registrados en el inventario.");
			return;
		}

		Console.WriteLine($"{"ID",-5}{"Producto",-28}{"Precio",15}{"Stock",10}  Estado");
		Console.WriteLine(new string('-', 72));

		for (int i = 0; i < nombres.Count; i++)
		{
			string alerta = stocks[i] < 5 ? "[ALERTA: BAJO STOCK]" : "";
			Console.WriteLine($"{i + 1,-5}{nombres[i],-28}{precios[i],15:C2}{stocks[i],10}  {alerta}");
		}
	}

	static void RegistrarVenta(List<string> nombres, List<decimal> precios, List<int> stocks, List<int> unidadesVendidas, ref int totalVentas, ref decimal totalCaja)
	{
		ImprimirEncabezado("REGISTRAR VENTA");

		if (nombres.Count == 0)
		{
			Console.WriteLine("No hay productos registrados para vender.");
			return;
		}

		for (int i = 0; i < nombres.Count; i++)
		{
			Console.WriteLine($"{i + 1}. {nombres[i]} | Precio: {precios[i]:C2} | Stock: {stocks[i]}");
		}

		int indice = LeerEntero($"Seleccione el producto (1-{nombres.Count}): ", 1, nombres.Count) - 1;
		int cantidad;

		do
		{
			cantidad = LeerEntero("Ingrese la cantidad a comprar: ", 1, int.MaxValue);

			if (cantidad > stocks[indice])
			{
				Console.WriteLine($"[ERROR] Stock insuficiente. Solo quedan {stocks[indice]} unidades.");
			}
		} while (cantidad > stocks[indice]);

		bool tieneDescuento = LeerSiNo("¿Aplica descuento de cliente frecuente (10%)? (S/N): ");
		decimal subtotal = precios[indice] * cantidad;
		decimal total = CalcularFactura(precios[indice], cantidad, tieneDescuento, out decimal montoIva, out decimal montoDescuento);

		stocks[indice] -= cantidad;
		unidadesVendidas[indice] += cantidad;
		totalVentas++;
		totalCaja += total;

		ImprimirEncabezado("TICKET DE VENTA");
		Console.WriteLine($"Producto:              {nombres[indice]} (x{cantidad})");
		Console.WriteLine($"Subtotal:              {subtotal:C2}");
		Console.WriteLine($"Descuento (10%):      -{montoDescuento:C2}");
		Console.WriteLine($"IVA (19%):            +{montoIva:C2}");
		Console.WriteLine("----------------------------------------------------");
		Console.WriteLine($"TOTAL A PAGAR:         {total:C2}");
		Console.WriteLine("====================================================");
		Console.WriteLine($"[OK] Venta efectuada. Stock actualizado: {stocks[indice]} unidades.");
	}

	static bool LeerSiNo(string mensaje)
	{
		string respuesta;

		do
		{
			Console.Write(mensaje);
			respuesta = (Console.ReadLine() ?? string.Empty).Trim().ToLower();

			if (respuesta != "s" && respuesta != "n")
			{
				Console.WriteLine("[ERROR] Responda únicamente S o N.");
			}
		} while (respuesta != "s" && respuesta != "n");

		return respuesta == "s";
	}

	static void MostrarReporte(List<string> nombres, List<int> unidadesVendidas, int totalVentas, decimal totalCaja)
	{
		ImprimirEncabezado("REPORTE DE CAJA Y ESTADÍSTICAS");
		decimal promedio = totalVentas > 0 ? totalCaja / totalVentas : 0m;
		Console.WriteLine($"Total de ventas realizadas: {totalVentas}");
		Console.WriteLine($"Total ingresado a caja:     {totalCaja:C2}");
		Console.WriteLine($"Promedio por venta:         {promedio:C2}");

		if (totalVentas == 0)
		{
			Console.WriteLine("Producto más vendido:       No hay ventas registradas.");
			return;
		}

		int indiceMayor = 0;
		for (int i = 1; i < unidadesVendidas.Count; i++)
		{
			if (unidadesVendidas[i] > unidadesVendidas[indiceMayor])
			{
				indiceMayor = i;
			}
		}

		Console.WriteLine($"Producto más vendido:       {nombres[indiceMayor]} ({unidadesVendidas[indiceMayor]} unidades)");
	}
}
