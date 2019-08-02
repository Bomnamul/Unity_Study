/*
 * https://chulin28ho.tistory.com/359
 * https://polycount.com/discussion/170394/technical-study-overwatch-image-heavy
 * 
 * Normal vector(-1 ~ 1) <=> Color vector(0 ~ 1)
 * => (x + 1) / 2 = x * 0.5 + 0.5
 * <= (r * 2) - 1
 * 
 * z = sqr(1 - x^2 + y^2)
 * x^2 + y^2 + z^2 = 1
 * 
 * a dot b = a.x * b.x + a.y * b.y = cos@ (길이가 1일 경우(Normal vector))
 * 똑같은 Vector의 내적
 * a dot a = x^2 + y^2
 */