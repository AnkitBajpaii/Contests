
Your challenge is to implement a string encoding function that will parse input strings into the desired output strings.

 

BUSINESS REQUIREMENTS:

Vowels are replaced with number: a -> 1, e -> 2, i -> 3, o -> 4, and u -> 5
Consonants are replaced with previous letter: b -> a, c -> b, d -> c, etc.
y is replaced with space
space is replaced with y
Numbers are replaced with their reverse: 1 -> 1, 23 -> 32, 1234 -> 4321
Other characters remain unchanged (punctuation, etc.)
All output should be lower case, regardless of input case ("Hello World" should produce the same result as "hello world")
