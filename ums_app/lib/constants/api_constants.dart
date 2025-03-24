class ApiConstants {
  static const String baseUrl = 'http://192.168.2.96:5039/api/';

  static const String login = '${baseUrl}Authentication/login';
  static const String validateOtp = '${baseUrl}Authentication/validate-otp';
  static const String getUser = '${baseUrl}User/getUser';
  static const String getApplications = '${baseUrl}Application/getApplications';
}
