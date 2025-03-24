class OtpValidationResponse {
  final String token; // JWT token

  OtpValidationResponse({required this.token});

  factory OtpValidationResponse.fromJson(Map<String, dynamic> json) {
    return OtpValidationResponse(token: json['token'] as String);
  }
}
