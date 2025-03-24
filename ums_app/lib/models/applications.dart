class Application {
  final int id;
  final int type_id;
  final int zone_id;
  final String observations;
  final int approver_id;
  final int applicant_id;
  final DateTime created_at;
  final DateTime updated_at;
  final bool approved;

  Application({
    required this.id,
    required this.type_id,
    required this.zone_id,
    required this.observations,
    required this.approver_id,
    required this.applicant_id,
    required this.created_at,
    required this.updated_at,
    required this.approved,
  });

  factory Application.fromJson(Map<String, dynamic> json) {
    return Application(
      id: json['id'],
      type_id: json['type_id'],
      zone_id: json['zone_id'],
      observations: json['observations'],
      approver_id: json['approver_id'],
      applicant_id: json['applicant_id'],
      created_at: DateTime.parse(json['created_at']),
      updated_at: DateTime.parse(json['updated_at']),
      approved: json['approved'],
    );
  }
}
