using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Readme : MonoBehaviour
{

}

/* https://www.hallgrimgames.com/blog/2018/10/16/unity-layout-groups-explained
 * 
 * Vertical Layout Group
 * 
 *     Control Child Size
 *     
 *     1. 자식 객체의 크기를 부모가 제어한다.
 *     2. 대부분 0의 초기값을 가짐, 단 Image/Text는 예외, Texture를 가지면 해당 크기가 사용 됨.
 *     3. 자식의 Height Field는 Disabled 되어 조정 불가.
 * 
 * 
 *     Child Force Expand
 *     
 *     1. 사용하지 않은 공간(Unused Space)이 있다면 자식을 확장해서 채움.
 *     2. Unused Space: 부모 Height - 모든 자식들의 Preferred/Min Height의 합.
 * 
 * 
 *     Layout Element
 *     
 *     1. 객체의 크기에 대한 정보를 제공, 기존 정보를 Override 시킴.
 *     2. Min, Preferred, Flexible
 *     3. Flexible: 단위가 Ratio(Weight)
 *     4. Image/Text Components는 Preferred 값이 자동으로 설정 됨. 단 Texture가 할당된 경우만 가능.
 *     5. Flexible이 제대로 동작하려면 부모의 Child Force Expand를 Off 해야 한다.
 */
