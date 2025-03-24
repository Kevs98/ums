import 'package:equatable/equatable.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:ums_app/models/applications.dart';
import 'package:ums_app/repositories/application_repository.dart';

part 'applications_event.dart';
part 'applications_state.dart';

class ApplicationsBloc extends Bloc<ApplicationsEvent, ApplicationsState> {
  final ApplicationRepository applicationRepository;

  ApplicationsBloc({required this.applicationRepository}) : super(ApplicationsInitial()) {
    on<GetApplicationsEvent>((event, emit) async {
      try {
        final applications = await applicationRepository.getApplications();
        emit(GetApplicationSuccess(applications.Data));
      } catch (e) {
        emit(ApplicationsFailure(e.toString()));
      }
    });
  }
}
