main(List<String> args) {
  for (var i = 0; i < 10; i++) {
    print("rodou $i");
  }

  int count = 0;
  while (count <= 10) {
    print("rodou while $count");
    count ++;
  }

}