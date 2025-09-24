Program: WordFinder
Objetivo
Implementar una clase WordFinder 
que identifique las 10 palabras más frecuentes
 de un flujo de datos que están presentes en una matriz de caracteres,
 considerando apariciones (1) horizontales y (2) verticales

Restricciones:
Entradas
Matriz:64×64 chars max. as IEnumerable<string>
WordStream: Datos<string> quizas grande de palabras a buscar en la matriz.

Definiciones:
Find: Las palabras se deben buscar l->r y t->b.
Conteo:
CONTAR = Verificar Existencia + Acumular Frecuencia
Paso 1: Verificar Existencia (¿La palabra está en la matriz?)
 Una palabra "existe" si aparece al menos una vez en la matriz (horizontal o vertical)
 No importa si aparece múltiples veces/orientaciones → cuenta como 1 existencia
Resultado binario: Existe/ No existe

Paso 2: Acumular Frecuencia (¿Cuántas veces aparece en el stream?)
Por cada aparición en el stream donde la palabra existe → +1 al contador

Stream con duplicados: Cada aparición única incrementa la frecuencia

Ranking final: Se ordena por este contador de frecuencia



Conteo de Frecuencia Idea 2:
a.Contar cada aparición en wordstream para determinar "most repeated words"
b.Si una palabra existe múltiples veces en la matriz,
 cuenta como una sola existencia.

Resultado: Top 10 palabras por frecuencia de aparición en el stream

Caso vacío: Retornar colección vacía si no hay coincidencias

⚡ Requisitos de Performance
Alta eficiencia para flujos de palabras grandes

Optimización en uso de recursos del sistema

Algoritmo eficiente considerando el tamaño limitado de la matriz (64×64)

🔍 Reglas de Búsqueda
Existencia en Matriz
text
Matriz Ejemplo:
c o l d
w i n d  
h o t x

"cold" → ✓ (horizontal, fila 1)
"wind" → ✓ (horizontal, fila 2) 
"down" → ✗ (no encontrada)
Conteo de Frecuencias
text
WordStream: ["cold", "cold", "wind", "hot", "cold", "heat"]

Frecuencias:
- "cold" → 3 (aparece 3 veces en stream, existe en matriz)
- "wind" → 1 (aparece 1 vez en stream, existe en matriz)  
- "hot" → 1 (aparece 1 vez en stream, existe en matriz)
- "heat" → 0 (no existe en matriz)

Resultado: ["cold", "wind", "hot"]

