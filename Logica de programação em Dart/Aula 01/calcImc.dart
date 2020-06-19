import 'dart:io';
main(){
  print("Digite seu peso");
  String weightInput = stdin.readLineSync();
  double weight = double.parse(weightInput);

  print("Digite sua altura");
  String heightInput = stdin.readLineSync();
  double height = double.parse(heightInput);

  var imc = calculateImc(weight,height);
  print("=============================");

  printResult(imc);
}

void printResult(double imc) {
  if(imc < 18.5){
    print("Abaixo do peso");
  } else if(imc > 18.5 && imc < 24.9){
    print("Peso Normal");
  } else if(imc > 25 && imc < 29.9){
    print("Sobrepeso");
  } else if(imc > 30 && imc < 34.9){
    print("Obesidade grau1");
  } else if(imc > 35 && imc < 39.9){
    print("Obesidade grau 2");
  } else{
    print("Obesidade grau 3");
  }
}

double calculateImc(double weight, double height){
  return weight / (height * height);
}