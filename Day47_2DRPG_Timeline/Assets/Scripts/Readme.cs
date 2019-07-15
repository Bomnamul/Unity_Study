/*
 * Timeline 시 주의 사항
 * 
 *  1. Animation timeline track에 들어간 객체는 Animator가 자동으로 생성이 된다. Animator controller가 없어도 제거하면 안된다.
 * 
 *  2. Animation용으로 사용되는 Animator랑 중복으로 사용할 수 없다. 하위 Model로 분리해야 한다.
 * 
 *  3. 실시간 생성되는(Instantiate) 객체는 실행 시점에 Binding 해야 한다.
 * 
 *  4. 실시간에 생성되는 객체에 대한 Timeline은 Local transform 기준으로 Animation 작업을 해라. World zero로 옮겨서 작업해라.
 * 
 *  5. Track offset 값을 Clear 해라. Track/Clip 두 개가 있다. 모두 Clear.
 */
