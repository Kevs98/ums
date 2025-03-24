import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:ums_app/blocs/auth_bloc.dart';
import 'package:ums_app/repositories/auth_repository.dart';
import 'package:ums_app/repositories/user_repository.dart';
import 'package:ums_app/screens/splash_screen.dart';

void main() {
  runApp(const MainApp());
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => AuthBloc(authRepository: AuthRepository(), userRepository: UserRepository()),
      child: MaterialApp(
        title: 'UMS App',
        theme: ThemeData(
          primarySwatch: Colors.blue,
          fontFamily: 'Poppins',
          textTheme: TextTheme(
            titleLarge: TextStyle(fontSize: 20, fontWeight: FontWeight.bold),
            bodyLarge: TextStyle(fontSize: 16),
            bodyMedium: TextStyle(fontSize: 14),
          ),
        ),
        home: const SplashScreen(),
      ),
    );
  }
}
