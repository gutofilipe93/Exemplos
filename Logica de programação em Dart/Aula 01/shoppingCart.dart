import 'dart:io';
main(){
    bool conditional = true;
    List<String> products = [];
    while (conditional) {
      print("DIGITE NOME DE UM PRODUTO");
      String product = stdin.readLineSync();
      if(product == "sair"){
        print("---- PROGRAMA FINALIZADO ---");
        conditional = false;
      } else if(product == "imprimir"){        
        getProducts(products);
        print("\n");
      } else if (product == "remover"){
        deletarProduct(products);
      } else{
        products.add(product);
        print("\x1B[2J\x1B[0;0H");
      }
    }
}

void deletarProduct(List<String> products) {
  print("QUAL ITEM VOCÊ DESEJA REMOVER?");
  getProducts(products);
  int item = int.parse(stdin.readLineSync());
  products.removeAt(item);
}

void getProducts(List<String> products) {
  int count = 0;
  for (var product in products) {    
    print("ITEM ${count} - ${product} ");
    count++;
  }
}