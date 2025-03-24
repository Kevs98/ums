class User {
  final int id;
  final String name;
  final String last_name;
  final String email;
  final String password;
  final String username;
  final int role_id;
  final int zone_id;
  final String otpSecret;
  final String image;
  final DateTime birthDate;
  final String gender;

  User({
    required this.id,
    required this.name,
    required this.last_name,
    required this.email,
    required this.password,
    required this.username,
    required this.role_id,
    required this.zone_id,
    required this.otpSecret,
    required this.image,
    required this.birthDate,
    required this.gender,
  });

  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      id: json['id'],
      name: json['name'],
      last_name: json['last_name'],
      email: json['email'],
      password: json['password'],
      username: json['username'],
      role_id: json['role_id'],
      zone_id: json['zone_id'],
      otpSecret: json['otpSecret'],
      image: json['image'],
      birthDate: DateTime.parse(json['birthDate']),
      gender: json['gender'],
    );
  }
}
