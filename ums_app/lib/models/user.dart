class User {
  final String id;
  final String name;
  final String email;
  final String username;
  final String password;

  User({required this.id, required this.name, required this.email, required this.username, required this.password});

  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      id: json['id'],
      name: json['name'],
      email: json['email'],
      username: json['username'],
      password: json['password'],
    );
  }
}
