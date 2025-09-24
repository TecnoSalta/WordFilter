Program: WordFinder
Objetivo
Implementar una clase WordFinder 
que identifique las 10 palabras más frecuentes
de un stream de datos que están presentes en una matriz de caracteres,
considerando apariciones (1) horizontales y (2) verticales

Restricciones:
Entradas
Matriz:64×64 chars max. as IEnumerable<string>

WordStream: Datos(stream) quizas grande de palabras a buscar 
grande pero finito (sino no podemos calcular el top 10)

Happy path:
Find: Las palabras se deben buscar l->r y t->b.
Suponemos:
 "cat" ="CAT" (case insensitive)
 No sabemos nada de la frecuencia de aparición en el stream o su tamaño o su distribución.

Conteo:
CONTAR = Verificar Existencia + Acumular Frecuencia
Paso 1: Verificar Existencia (¿La palabra está en la matriz?)
Una palabra "existe" si aparece al menos una vez en la matriz (horizontal o vertical)

No importa si aparece múltiples veces/orientaciones → cuenta como 1 existencia

Resultado binario: ✓ Existe / ✗ No existe

Paso 2: Acumular Frecuencia (¿Cuántas veces aparece en el stream?)
Por cada aparición en el stream donde la palabra existe → +1 al contador

Stream con duplicados: Cada aparición única incrementa la frecuencia


Resultado: Top 10 palabras por frecuencia de aparición en el stream

Caso vacío: Retornar colección vacía si no hay coincidencias

Requisitos de Performance
1. Alta eficiencia para flujos de palabras grandes

2. Optimización en uso de recursos del sistema (memoria y procesadores).

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
Diseño de la Interface
csharp
public class WordFinder
{
    // Constructor: Preprocesa la matriz para optimizar búsquedas
    public WordFinder(IEnumerable<string> matrix) { ... }
    
    // Find: Retorna top 10 palabras más frecuentes del stream que existen en la matriz
    public IEnumerable<string> Find(IEnumerable<string> wordstream) { ... }
}
Criterios de Éxito
[]Correctitud: Identifica palabras horizontales/verticales exactamente

[] Performance: Óptima para streams grandes mediante preprocesamiento

[] Precisión: Ranking correcto del top 10 por frecuencia real del stream

[] Robustez: Manejo adecuado de casos edge (matriz vacía, stream vacío, etc.)


---

BenchmarkDotNet v0.15.4, Windows 11 (10.0.22631.5909/23H2/2023Update/SunValley3)
12th Gen Intel Core i7-12700H 2.30GHz, 1 CPU, 20 logical and 14 physical cores
.NET SDK 9.0.305
  [Host]     : .NET 8.0.20 (8.0.20, 8.0.2025.41914), X64 RyuJIT x86-64-v3 [AttachedDebugger]
  DefaultJob : .NET 8.0.20 (8.0.20, 8.0.2025.41914), X64 RyuJIT x86-64-v3


| Method                | Mean      | Error     | StdDev    | Gen0   | Gen1   | Allocated |
|---------------------- |----------:|----------:|----------:|-------:|-------:|----------:|
| ExtractAllWords_32x32 |  6.327 us | 0.1236 us | 0.1733 us | 1.7776 | 0.0534 |   21.8 KB |
| ExtractAllWords_64x64 | 17.986 us | 0.2426 us | 0.1894 us | 5.6458 | 0.4578 |   69.2 KB |

Análisis de Rendimiento - Extracción de Palabras en Matriz

Resultados del Benchmark:

Matriz 32x32: 6.327 μs y 21.8 KB de memoria asignada

Matriz 64x64: 17.986 μs y 69.2 KB de memoria asignada

Completo el análisis:

La velocidad de carga para las palabras extraídas horizontal y vertical es O(R*C) siendo R y C las filas y columnas de la matriz respectivamente. Ocupa poco espacio en memoria adicional porque utiliza un algoritmo eficiente que procesa cada celda una sola vez y almacena únicamente las palabras válidas encontradas, sin necesidad de estructuras de datos auxiliares complejas.

Relación de escalado:

Tiempo: Al duplicar las dimensiones (de 32x32 a 64x64), el tiempo aumenta aproximadamente 2.8x, lo que se aproxima al crecimiento cuadrático esperado O(n²) para una matriz

Memoria: La memoria asignada aumenta 3.17x, consistente con el crecimiento del área de la matriz (de 1024 a 4096 celdas)

Continuación: Ahora toca implementar la función Find
