import 'package:equatable/equatable.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:ums_app/models/user.dart';
import 'package:ums_app/repositories/auth_repository.dart';
import 'package:ums_app/repositories/user_repository.dart';

part 'auth_event.dart';
part 'auth_state.dart';

class AuthBloc extends Bloc<AuthEvent, AuthState> {
  final AuthRepository authRepository;
  final UserRepository userRepository;

  AuthBloc({required this.authRepository, required this.userRepository}) : super(AuthInitial()) {
    on<LoginEvent>((event, emit) async {
      emit(AuthLoading());
      try {
        final response = await authRepository.login(event.username, event.password);
        final user = await userRepository.getUser(event.username, event.password);
        emit(AuthSuccess(user.Data));
      } catch (e) {
        emit(AuthFailure(e.toString()));
      }
    });

    on<ValidateOtpEvent>((event, emit) async {
      emit(AuthLoading());
      try {
        final response = await authRepository.validateOtp(event.otp, event.otpSecret, event.username);
        emit(OtpValidated(response.Data.token));
      } catch (e) {
        emit(AuthFailure(e.toString()));
      }
    });
  }
}
