
import 'package:objeto/src/human.dart';

class Person extends Human {
  String name = 'Gustavo';
  String sex = 'M';
  int age = 27;

  String _localVar; // Em dart para dizer que uma variavel é privada usa-se '_' no inicio dela

  final String otherVar = 'Barbosa'; // Quando usa a palavra reservada 'final' significa que essa variavel só pode atribuir valores para ela uma unica vez

  Person({this.name, this.sex, this.age, weight, height}): super (weight: weight, height: height);
}