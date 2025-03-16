import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:ums_app/models/user.dart';

class AuthRepository {
  final String baseUrl = 'http://localhost:5000/api';

  Future<User> login(String username, String password) async {
    final response = await http.post(
      Uri.parse('$baseUrl/login'),
      body: jsonEncode({'username': username, 'password': password}),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      return User.fromJson(jsonDecode(response.body));
    } else {
      throw Exception('Failed to login');
    }
  }
}
