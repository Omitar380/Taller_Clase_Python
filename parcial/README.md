# Sistema Gestor de Ventas e Inventario (Mini-POS)

**Estudiante:** Completar con el nombre completo.

Aplicación de consola desarrollada en C# para registrar productos, consultar el inventario, procesar ventas con descuento e IVA, y consultar estadísticas de caja.

## Requisitos

- .NET SDK 8.0 o superior

## Ejecutar

Desde la carpeta del proyecto:

```bash
dotnet run
```

Para compilar:

```bash
dotnet build
```

## Funcionalidades

1. Registrar productos con nombre, precio y stock.
2. Consultar el inventario y alertas de bajo stock.
3. Registrar ventas con validación de existencias, descuento del 10% e IVA del 19%.
4. Consultar total de ventas, caja, promedio y producto más vendido.
5. Salir del sistema.

La información se mantiene en memoria mediante listas durante la sesión de ejecución.
