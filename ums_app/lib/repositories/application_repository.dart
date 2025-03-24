import 'dart:convert';

import 'package:http/http.dart' as http;
import 'package:ums_app/constants/api_constants.dart';
import 'package:ums_app/models/api_response.dart';
import 'package:ums_app/models/applications.dart';

class ApplicationRepository {
  Future<ApiResponse<List<Application>>> getApplications() async {
    final response = await http.get(
      Uri.parse(ApiConstants.getApplications),
      headers: {'Content-Type': 'application/json'},
    );

    if (response.statusCode == 200) {
      return ApiResponse.fromJson(
        jsonDecode(response.body),
        (data) => (data as List).map((item) => Application.fromJson(item)).toList(),
      );
    } else {
      throw Exception('Failed to get applications');
    }
  }
}
