import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:intl/intl.dart';
import 'package:ums_app/blocs/applications_bloc.dart';
import 'package:ums_app/models/applications.dart';
import 'package:ums_app/repositories/application_repository.dart';

class RequestsScreen extends StatefulWidget {
  const RequestsScreen({super.key});

  @override
  _RequestsScreenState createState() => _RequestsScreenState();
}

class _RequestsScreenState extends State<RequestsScreen> {
  String _filterStatus = 'No Aprobado';
  String _filterDate = 'Todos';

  String formatDate(DateTime date) {
    final formatter = DateFormat('dd/MM/yyyy HH:mm');
    return formatter.format(date);
  }

  List<Application> _filterApplications(List<Application> applications) {
    List<Application> filteredApplications =
        applications.where((app) {
          if (_filterStatus == 'Aprobado') {
            return app.approved;
          } else if (_filterStatus == 'No Aprobado') {
            return !app.approved;
          } else {
            return true;
          }
        }).toList();

    final now = DateTime.now();
    if (_filterDate == 'Ultimos 7 Dias') {
      filteredApplications =
          filteredApplications.where((app) {
            return app.created_at.isAfter(now.subtract(Duration(days: 7)));
          }).toList();
    } else if (_filterDate == 'Últimos 30 días') {
      filteredApplications =
          filteredApplications.where((app) {
            return app.created_at.isAfter(now.subtract(Duration(days: 30)));
          }).toList();
    }

    return filteredApplications;
  }

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create:
          (context) => ApplicationsBloc(applicationRepository: ApplicationRepository())..add(GetApplicationsEvent()),
      child: Scaffold(
        appBar: AppBar(
          title: Text('Solicitudes', style: TextStyle(color: Colors.white)),
          backgroundColor: Colors.blue[800],
          elevation: 4,
          iconTheme: IconThemeData(color: Colors.white),
          actions: [
            DropdownButton<String>(
              value: _filterStatus,
              onChanged: (String? newValue) {
                setState(() {
                  _filterStatus = newValue!;
                });
              },
              items:
                  <String>['Todo', 'Aprobado', 'No Aprobado'].map<DropdownMenuItem<String>>((String value) {
                    return DropdownMenuItem<String>(value: value, child: Text(value));
                  }).toList(),
              dropdownColor: Colors.white,
              icon: Icon(Icons.filter_list, color: Colors.white),
              underline: Container(),
            ),

            PopupMenuButton<String>(
              icon: Icon(Icons.calendar_today, color: Colors.white),
              onSelected: (String value) {
                setState(() {
                  _filterDate = value;
                });
              },
              itemBuilder: (BuildContext context) {
                return [
                  PopupMenuItem<String>(value: 'Todos', child: Text('Todos')),
                  PopupMenuItem<String>(value: 'Últimos 7 días', child: Text('Últimos 7 días')),
                  PopupMenuItem<String>(value: 'Últimos 30 días', child: Text('Últimos 30 días')),
                ];
              },
            ),
          ],
        ),
        body: BlocBuilder<ApplicationsBloc, ApplicationsState>(
          builder: (context, state) {
            if (state is ApplicationsLoading) {
              return Center(child: CircularProgressIndicator());
            } else if (state is GetApplicationSuccess) {
              final filteredApplications = _filterApplications(state.applications);
              return ListView.builder(
                padding: const EdgeInsets.all(12),
                itemCount: filteredApplications.length,
                itemBuilder: (context, index) {
                  final application = filteredApplications[index];
                  return Card(
                    elevation: 2,
                    margin: EdgeInsets.only(bottom: 16),
                    shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
                    child: ListTile(
                      contentPadding: EdgeInsets.all(16),
                      leading: Icon(Icons.assignment, color: Colors.blue[800]),
                      title: Text(
                        application.observations,
                        style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                      ),
                      subtitle: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          SizedBox(height: 4),
                          Text(
                            'Creado: ${formatDate(application.created_at)}',
                            style: TextStyle(fontSize: 12, color: Colors.grey[600]),
                          ),
                        ],
                      ),
                      trailing: Icon(
                        application.approved ? Icons.circle : Icons.circle,
                        color: application.approved ? Colors.green : Colors.red,
                        size: 16,
                      ),
                    ),
                  );
                },
              );
            } else if (state is ApplicationsFailure) {
              return Center(child: Text(state.error));
            } else {
              return Center(child: Text('No hay solicitudes'));
            }
          },
        ),
        floatingActionButton: FloatingActionButton(onPressed: () {}, child: Icon(Icons.add)),
      ),
    );
  }
}
