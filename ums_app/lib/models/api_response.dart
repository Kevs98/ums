class ApiResponse<T> {
  final int StatusCode;
  final String Message;
  final T Data;

  ApiResponse({required this.StatusCode, required this.Message, required this.Data});

  factory ApiResponse.fromJson(Map<String, dynamic> json, T Function(dynamic) fromJsonData) {
    return ApiResponse(
      StatusCode: json['StatusCode'] as int,
      Message: json['Message'] as String,
      Data: fromJsonData(json['Data']),
    );
  }
}
