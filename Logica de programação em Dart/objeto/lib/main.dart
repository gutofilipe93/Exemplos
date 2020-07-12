import 'src/person.dart';

void main(List<String> arguments) {
  var person = Person(name: 'Gustavo', age: 27, sex: 'M',height: 1.80, weight: 66.5);
  print(person.name);
  print(person.age);
  print(person.sex);

  //Herança
  print(person.height);
  print(person.weight);
}
