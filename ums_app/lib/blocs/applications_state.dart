part of 'applications_bloc.dart';

abstract class ApplicationsState extends Equatable {
  const ApplicationsState();

  @override
  List<Object> get props => [];
}

class ApplicationsInitial extends ApplicationsState {}

class ApplicationsLoading extends ApplicationsState {}

class GetApplicationSuccess extends ApplicationsState {
  final List<Application> applications;

  const GetApplicationSuccess(this.applications);

  @override
  List<Object> get props => [applications];
}

class ApplicationsFailure extends ApplicationsState {
  final String error;

  const ApplicationsFailure(this.error);

  @override
  List<Object> get props => [error];
}
