import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:ums_app/constants/api_constants.dart';
import 'package:ums_app/models/api_response.dart';
import 'package:ums_app/models/user.dart';

class UserRepository {
  Future<ApiResponse<User>> getUser(String username, String password) async {
    final urlFinal = Uri.parse(
      ApiConstants.getUser,
    ).replace(queryParameters: {'username': username, 'password': password});
    final response = await http.get(urlFinal, headers: {'Content-Type': 'application/json'});

    if (response.statusCode == 200) {
      return ApiResponse.fromJson(jsonDecode(response.body), (data) => User.fromJson(data));
    } else {
      throw Exception('Failed to get user');
    }
  }
}
