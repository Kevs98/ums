class LoginData {
  final String message;
  final String otpSecret;

  LoginData({required this.message, required this.otpSecret});

  factory LoginData.fromJson(Map<String, dynamic> json) {
    return LoginData(message: json['message'], otpSecret: json['otpSecret']);
  }
}
