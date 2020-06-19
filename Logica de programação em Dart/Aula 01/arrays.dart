import 'dart:io';
main(List<String> args) {
  var names = [];

  bool flag = true;
  while (flag) {
    print("Digite seu nome");
    String text = stdin.readLineSync();
    if(text == "sair"){
      print("====== PROGRAMA FINALIZADO ====");
      flag = false;
    } else {
      names.add(text);
    }
  }

  print(names);


}