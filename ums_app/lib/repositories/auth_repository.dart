import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:ums_app/constants/api_constants.dart';
import 'package:ums_app/models/api_response.dart';
import 'package:ums_app/models/login_response.dart';
import 'package:ums_app/models/otp_validation_response.dart';

class AuthRepository {
  Future<ApiResponse<LoginResponse>> login(String username, String password) async {
    final response = await http.post(
      Uri.parse(ApiConstants.login),
      body: jsonEncode({'username': username, 'password': password}),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      return ApiResponse.fromJson(jsonDecode(response.body), (data) => LoginResponse.fromJson(data));
    } else {
      throw Exception('Failed to login');
    }
  }

  Future<ApiResponse<OtpValidationResponse>> validateOtp(String otp, String otpSecret, String username) async {
    final response = await http.post(
      Uri.parse(ApiConstants.validateOtp),
      body: jsonEncode({'OtpCode': otp, 'SecretKey': otpSecret, 'Username': username}),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      return ApiResponse.fromJson(jsonDecode(response.body), (data) => OtpValidationResponse.fromJson(data));
    } else {
      throw Exception('Failed to validate OTP');
    }
  }
}
