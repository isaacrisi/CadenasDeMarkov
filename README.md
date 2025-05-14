Proyecto: Generación de Texto con Cadenas de Markov
1. Corpus utilizado
Se utilizó un conjunto de canciones del artista WOS, abarcando temas de diferentes álbumes y proyectos musicales. La elección de este corpus se debe a la riqueza léxica, el uso de metáforas, la diversidad temática y la estructura narrativa presentes en sus letras.

El corpus incluye 23 canciones, cada una con una extensión superior a las 200 palabras, permitiendo generar un vocabulario amplio y representativo para la modelación mediante Cadenas de Markov de primer orden.
2. Distribución de Probabilidad Inicial
Se construyó la distribución de probabilidad inicial a partir de la frecuencia de las palabras que aparecen como primeras en cada canción/texto. A continuación se presenta un histograma con las palabras más frecuentes al inicio:

 
3. Preprocesamiento realizado
- Conversión de todo el texto a minúsculas.
- Normalización de caracteres con tildes (á, é, í, ó, ú) a sus equivalentes sin tilde.
- Eliminación de signos de puntuación.
- Separación del texto en tokens (palabras).
- Eliminación de saltos de línea.
- Estructuración de las parejas de palabras (bigramas) para la matriz de transición.
 
4. Construcción del Espacio de Estados
El espacio de estados se definió como el conjunto de todas las palabras únicas presentes en el corpus. Se construyó un mapeo entre palabras y sus respectivos índices para la creación de la matriz de transición.
 

 
5. Construcción de la Matriz de Transición
Se calcularon las frecuencias de aparición de todas las parejas consecutivas de palabras en el corpus. Estas frecuencias se normalizaron para obtener la matriz de transición de probabilidades.
 

 

6. Clases de Comunicación
En el análisis de las clases de comunicación de la Cadena de Markov generada a partir del corpus de canciones de WOS, se identificaron dos clases:
1.	Una clase principal, que incluye la gran mayoría de las palabras del vocabulario.
2.	Una clase aislada, compuesta únicamente por la palabra "wow".
Explicación:
•	La clase principal agrupa casi todas las palabras, lo cual es un comportamiento esperado en este tipo de corpus.
Al tratarse de letras de canciones con estructuras repetitivas, conexiones semánticas y variedad de transiciones, es común que la mayoría de los estados (palabras) terminen comunicándose entre sí directa o indirectamente.
Esto refleja que desde cualquier palabra inicial se puede, con probabilidad no nula y en algún número de pasos, alcanzar cualquier otra palabra de la clase, y viceversa.
•	La clase aislada "wow" corresponde a una palabra que, en el corpus utilizado, no tiene transiciones de ida y vuelta con las demás palabras.
Puede ser el resultado de su aparición aislada en un contexto sin conexión, por ejemplo como interjección en un final de verso, lo que la deja fuera de las rutas de comunicación del resto del modelo.
Justificación:
Este resultado es coherente con el comportamiento típico de las Cadenas de Markov en modelos de generación de texto. Las clases de comunicación reflejan la conectividad del vocabulario en función de la estructura de las letras, lo cual en el caso del rap suele ser altamente interconectado, salvo por elementos aislados como expresiones sueltas.
Evidencia:
A continuación se muestra una captura del resultado obtenido al ejecutar el análisis de clases de comunicación sobre la matriz de transiciones del proyecto.
 
 




7. Periodicidad de Estados
En el análisis realizado sobre el modelo generado con las letras de WOS, se observó que:
•	Todos los estados relevantes (palabras) tienen periodo igual a 1.
•	Esto significa que la cadena es aperiódica.
Justificación:
Este resultado era esperable, ya que:
•	El corpus está compuesto por textos naturales (letras de canciones), donde no existen ciclos estrictos que obliguen a pasar siempre por una cantidad fija de pasos antes de volver a una palabra.
•	La presencia de múltiples caminos, repeticiones y frases con diferentes longitudes rompe cualquier patrón de periodicidad fija.
Por tanto, los estados pueden ser visitados nuevamente en diferentes momentos, lo cual garantiza la aperiodicidad del modelo.
Implicaciones:
La aperiodicidad es una propiedad deseable para la generación de texto, ya que:
•	Aumenta la flexibilidad en la generación de secuencias.
•	Permite alcanzar el comportamiento estacionario de la cadena con mayor facilidad.

8. Probabilidades a n Pasos (Chapman-Kolmogorov)
Se calculó la matriz de transición elevada a 1 paso para analizar la evolución de las probabilidades y evitar muchos calculos  y que salga un resultado normal

Resultados:
- Para confirmar que wow es una clase unica y se analizo la probabilidad de ir desde wow hasta camina en 1 paso y la probabilidad fue de 0.
 



- Ya para mirar la probabilidad de una palabra mas comun como lo es ell aver que probabilidad tiene para ir a camina en 1 paso es de 
 


9. Ejemplos de Secuencias Generadas
 

 
10. Conclusiones
•	La matriz de transición obtenida refleja de manera clara las estructuras repetitivas y los patrones lingüísticos característicos de las letras de WOS.
Las conexiones entre palabras muestran cómo ciertos términos tienden a agruparse, generando secuencias frecuentes que son identificables en su estilo lírico.
•	El análisis de clases de comunicación evidenció una clase dominante que abarca la mayor parte del vocabulario, lo cual es consistente con la naturaleza continua y entrelazada de las letras de canciones.
Asimismo, se identificó una clase aislada (la palabra "wow") debido a su uso singular y sin conexiones bidireccionales.
•	La cadena de Markov construida resultó ser aperiódica, ya que no se encontraron ciclos con periodicidad fija en los estados.
Esto garantiza que los estados (palabras) pueden ser revisitados en diferentes momentos, favoreciendo la generación flexible de texto.
•	El análisis de Chapman-Kolmogorov permitió observar cómo las probabilidades de transición se diluyen y estabilizan conforme se incrementa el número de pasos.
Las palabras de alta frecuencia mantienen una probabilidad significativa, mientras que las de uso esporádico tienden a perder influencia en recorridos largos.
•	Las secuencias de texto generadas a partir del modelo muestran coherencia a corto plazo, formando frases con sentido parcial, pero con limitaciones semánticas en secuencias más extensas.
Esto es inherente a las Cadenas de Markov de primer orden, donde las transiciones solo dependen del estado actual.
•	El uso de Cadenas de Markov permitió analizar de manera probabilística el estilo y las estructuras narrativas de las letras de WOS, ofreciendo una herramienta para la exploración de patrones textuales y la generación automática de contenido.
Sin embargo, se reconoce que para lograr mayor coherencia semántica sería necesario incorporar modelos de mayor orden o enfoques más avanzados.

11. Repositorio con todo el Proyecto:
