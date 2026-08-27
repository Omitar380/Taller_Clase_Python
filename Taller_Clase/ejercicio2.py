numero_a = 10
numero_b = 3
print("\nOperadores aritmeticos:")
print(f"10 + 3 = {numero_a + numero_b}")
print(f"10 - 3 = {numero_a - numero_b}")
print(f"10 * 3 = {numero_a * numero_b}")
print(f"10 / 3 = {numero_a / numero_b}")
print(f"10 // 3 = {numero_a // numero_b}")
print(f"10 % 3 = {numero_a % numero_b}")
print(f"10 ** 3 = {numero_a ** numero_b}")
print("\nOperadores de comparacion:")
print(f"10 == 3: {numero_a == numero_b}")
print(f"10 != 3: {numero_a != numero_b}")
print(f"10 > 3: {numero_a > numero_b}")
print(f"10 < 3: {numero_a < numero_b}")
print(f"10 >= 3: {numero_a >= numero_b}")
print(f"10 <= 3: {numero_a <= numero_b}")
print("\nOperadores logicos y de pertenencia:")
es_mayor = numero_a > numero_b
es_positivo = numero_a > 0
print(f"True and True: {es_mayor and es_positivo}")
print(f"True or False: {es_mayor or False}")
print(f"not False: {not False}")
print(f"'a' in 'casa': {'a' in 'casa'}")
print(f"'z' not in 'casa': {'z' not in 'casa'}")
print("\nOperadores bit a bit:")
print(f"10 & 3 = {numero_a & numero_b}")
print(f"10 | 3 = {numero_a | numero_b}")
print(f"10 ^ 3 = {numero_a ^ numero_b}")
print(f"~10 = {~numero_a}")
print(f"10 << 1 = {numero_a << 1}")
print(f"10 >> 1 = {numero_a >> 1}")
print("\nOperadores de asignacion e identidad:")
valor = 8
valor += 2
print(f"valor = 8; valor += 2 -> {valor}")
valor -= 1
print(f"valor -= 1 -> {valor}")
valor *= 2
print(f"valor *= 2 -> {valor}")
valor /= 2
print(f"valor /= 2 -> {valor}")
referencia = numero_b
print(f"referencia is numero_b: {referencia is numero_b}")
print(f"referencia is not numero_a: {referencia is not numero_a}")
print("\nOtros simbolos frecuentes:")
lista = ["Python", "es", "genial"]
datos = {"lenguaje": "Python", "nivel": "basico"}
print(f"Lista [ ]: {lista}")
print(f"Diccionario {{ }}: {datos}")
print(f"Tupla ( ): {(1, 2, 3)}")
print(f"Dos puntos : y coma ; punto . coma , arroba @")
print("Comillas simples ' y dobles \"")
print("Barra invertida \\ y almohadilla #")
