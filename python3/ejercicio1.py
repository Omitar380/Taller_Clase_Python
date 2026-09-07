ARCHIVO_USUARIOS = "usuarios.txt"


def cargar_usuarios():
    usuarios = []
    try:
        with open(ARCHIVO_USUARIOS, "r", encoding="utf-8") as archivo:
            for linea in archivo:
                campos = linea.strip().split(";")
                if len(campos) >= 3 and campos[0] and campos[1] and campos[2]:
                    try:
                        usuarios.append({
                            "nombre": campos[0],
                            "id": campos[1],
                            "edad": int(campos[2]),
                        })
                    except ValueError:
                        pass
    except FileNotFoundError:
        pass
    return usuarios


def guardar_usuarios(usuarios):
    with open(ARCHIVO_USUARIOS, "w", encoding="utf-8") as archivo:
        for usuario in usuarios:
            archivo.write(
                f"{usuario['nombre']};{usuario['id']};{usuario['edad']}\n"
            )


def mostrar_usuario(usuario):
    print(f"Nombre: {usuario['nombre']} | Id: {usuario['id']} | Edad: {usuario['edad']}")


def pedir_edad(mensaje, edad_actual=None):
    while True:
        prompt = f"{mensaje} [{edad_actual}]: " if edad_actual is not None else mensaje
        entrada = input(prompt).strip()
        
        
        if edad_actual is not None and not entrada:
            return edad_actual
            
        try:
            edad = int(entrada)
            if edad >= 0:
                return edad
            print("La edad no puede ser negativa.")
        except ValueError:
            print("La edad debe ser un número entero.")


def crear_usuario(usuarios):
    nombre = input("Nombre: ").strip()
    identificador = input("Id: ").strip()

    if not nombre or not identificador:
        print("El nombre y el id son obligatorios.")
        return
    if any(usuario["id"] == identificador for usuario in usuarios):
        print("No se puede crear otro usuario con el mismo id.")
        return

    edad = pedir_edad("Edad: ")
    usuarios.append({"nombre": nombre, "id": identificador, "edad": edad})
    guardar_usuarios(usuarios)
    print("Usuario creado correctamente.")


def listar_por_edad(usuarios):
    edad = pedir_edad("Edad a consultar: ")
    encontrados = [usuario for usuario in usuarios if usuario["edad"] == edad]
    if not encontrados:
        print("No hay usuarios con esa edad.")
        return
    for usuario in encontrados:
        mostrar_usuario(usuario)


def listar_en_orden_de_edad(usuarios):
    if not usuarios:
        print("No hay usuarios registrados.")
        return
    for usuario in sorted(usuarios, key=lambda elemento: elemento["edad"]):
        mostrar_usuario(usuario)


def buscar_usuario(usuarios):
    identificador = input("Id a buscar: ").strip()
    usuario = next(
        (usuario for usuario in usuarios if usuario["id"] == identificador), None
    )
    if usuario is None:
        print("Usuario no encontrado.")
    else:
        mostrar_usuario(usuario)


def modificar_usuario(usuarios):
    identificador = input("Id del usuario a modificar: ").strip()
    usuario = next(
        (usuario for usuario in usuarios if usuario["id"] == identificador), None
    )
    if usuario is None:
        print("Usuario no encontrado.")
        return

    nuevo_nombre = input(f"Nombre [{usuario['nombre']}]: ").strip()
    nuevo_id = input(f"Id [{usuario['id']}]: ").strip()
    
    if not nuevo_nombre:
        nuevo_nombre = usuario["nombre"]
    if not nuevo_id:
        nuevo_id = usuario["id"]
        
    if nuevo_id != usuario["id"] and any(
        otro["id"] == nuevo_id for otro in usuarios
    ):
        print("No se puede modificar al id de otro usuario existente.")
        return

    usuario["nombre"] = nuevo_nombre
    usuario["id"] = nuevo_id
    usuario["edad"] = pedir_edad("Edad", edad_actual=usuario["edad"])
    guardar_usuarios(usuarios)
    print("Usuario modificado correctamente.")


def main():
    usuarios = cargar_usuarios()
    opciones = {
        "1": ("Crear", crear_usuario),
        "2": ("Listar por edad", listar_por_edad),
        "3": ("Listar en orden de edad", listar_en_orden_de_edad),
        "4": ("Buscar", buscar_usuario),
        "5": ("Modificar", modificar_usuario),
    }

    while True:
        print("\n--- Gestión de usuarios ---")
        for numero, (descripcion, _) in opciones.items():
            print(f"{numero}. {descripcion}")
        print("0. Salir")
        opcion = input("Seleccione una opción: ").strip()

        if opcion == "0":
            print("Programa finalizado.")
            break
        if opcion in opciones:
            opciones[opcion][1](usuarios)
        else:
            print("Opción no válida.")


if __name__ == "__main__":
    main()