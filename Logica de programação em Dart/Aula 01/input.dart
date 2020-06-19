import 'dart:io';

main(){
  print("==== Digite a sua idade ===");
  var input = stdin.readLineSync();
  var age = int.parse(input);

  if(age >= 50){
    print("melhor idade");
  } else if(age >= 18){
    print("adulto");
  } else if(age >= 12) {
    print("adolecente");
  } else {
    print("criança");
  } 

}