import 'dart:io';

Map<String, dynamic> register = {};
List<Map<String, dynamic>> registers = [];
main(List<String> args) {
  bool conditional = true;
  print("\x1B[2J\x1B[0;0H");
  while (conditional) {
    print("DIGITE UM COMANDO");
    String comand = stdin.readLineSync();
    if (comand == "sair") {
      print("---- PROGRAMA FINALIZADO ---");
      conditional = false;
    } else if (comand == "cadastrar") {
      print("\x1B[2J\x1B[0;0H");
      Add();
    } else if (comand == "imprimir") {
      print(registers);
      print("\n");
    } else {
      print("ESSE COMANDO NÃO EXISTE");
    }
  }
}

void Add() {
  addDataPerson("nome");
  addDataPerson("idade");
  addDataPerson("cidade");
  addDataPerson("estado");
  registers.add(register);
}

void addDataPerson(String value) {
  print("Digite o seu ${value}");
  String data = stdin.readLineSync();
  register["${value}"] = data;
}
