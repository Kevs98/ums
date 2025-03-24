class LoginResponse {
  final String message;
  final String otpSecret;

  LoginResponse({required this.message, required this.otpSecret});

  factory LoginResponse.fromJson(Map<String, dynamic> json) {
    return LoginResponse(message: json['message'] as String, otpSecret: json['otpSecret'] as String);
  }
}
