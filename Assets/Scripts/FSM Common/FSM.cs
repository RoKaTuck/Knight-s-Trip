//=========================================================================
/*
 * [ 참고 : https://blog.naver.com/jerrypoiu/221235988023
 *			http://www.ktword.co.kr/word/abbr_view.php?m_temp1=3203
 *			https://boycoding.tistory.com/262
 *			https://blog.naver.com/debuff9710/221333379009 ]
 *			 
 *	------------------------
 *	유한 상태 머신...
 *	------------------------
 *		
 *		-----------
 *		-	정의..
 *		-----------
 *		
 *			-	유한 개의 상태를 가지고 주어지는 입력에 따라
 *				어떤 상태에서 다른 상태로 전환하거나
 *				출력이나 액션이 일어나게 하는 시스템..
 *				
 *
 *		---------------
 *		-	기본 개념..
 *		---------------
 *			
 *			-	[ 상태 ]	:	현재 진행중인 동작이나 기능..
 *			-	[ 전이 ]	:	현재 [ 상태 ]에서 다른 [ 상태 ]로 바뀌는 것..
 *			-	[ 이벤트 ]	:	[ 전이 ]를 위한 조건..
 *	
 *	
 *	
 *		------------
 *		-	특징..
 *		------------
 *		
 *			-	[ 상태 ]의 갯수가 유한하다..
 *			-	한번에 하나의 [ 상태 ]만 관리 가능..
 *			
 *			
 *		------------
 *		-	장단점
 *		------------
 *		
 *			------------
 *			-	장점..
 *			------------
 *			
 *				-	오류 수정이 용이..
 *					-	각 상태가 모듈화 되어있으므로 오류의 원인을 찾기 쉬움..
 *					
 *					
 *				-	직관적..
 *					-	상태들이 독립적으로 구성되어 있으므로 
 *						[ 전이 ]나 [ 이벤트 ]를 파악하기 쉬움..
 *						
 *					-	구조가 단순하여 행동의 결과 예측이 가능..
 *					
 *					
 *				-	유연성..
 *					-	새로운 상태를 추가하거나 [ 전이 ]관계를 수정하는 것이 용이..
 *					
 *				
 *			------------
 *			-	단점..
 *			------------
 *				
 *				-	[ 상태 ]의 규모가 커지면 설계가 복잡해짐..
 *				-	제한된 범위의 문제에만 적용 가능..
 *				-	단순 계산 작업에는 부적합..
 *				 
 */
//=========================================================================
using UnityEngine;
//=========================================================================
//	FSM ( Finite State Machine.. )
public class FSM <T>  : MonoBehaviour
{
	//---------------------------------------
	private T owner;	//	상태 소유자..
	private IFSMState<T> currentState = null;	//	현재 상태..
	private IFSMState<T> previousState = null;	//	이전 상태..
	//---------------------------------------
	public IFSMState<T> CurrentState{ get {return currentState;} }
	public IFSMState<T> PreviousState{ get {return previousState;} }
	//---------------------------------------
	//	초기 상태와 상태 소유자 설정..
	protected void InitState(T owner, IFSMState<T> initialState)
	{
		this.owner = owner;
		ChangeState(initialState);
	}
	//---------------------------------------
	//	각 상태의 실시간 처리..
	protected void  FSMUpdate() { if (currentState != null) currentState.Execute(owner); }
	//--------------------------------------- 
	//	상태 변경..
	public void  ChangeState(IFSMState<T> newState)
	{
		//	이전 상태 교체..
		previousState = currentState;
 
		//	이전 상태 종료!!
		if (previousState != null)
			previousState.Exit(owner);
 
		//	현재 상태 교체..
		currentState = newState;

		//	현재 상태 시작!!
		if (currentState != null)
			currentState.Enter(owner);
	
	}//	public void  ChangeState(IState<T> newState)
	//--------------------------------------- 
	//	이전 상태로 전환..
	public void  RevertState()
	{
		if (previousState != null)
			ChangeState(previousState);
	
	}//	public void  RevertState()
	//---------------------------------------
	//	디버깅용...
	//	-	현재상태 확인..
	public override string ToString() { return currentState.ToString(); }
	//---------------------------------------

}//	public class FSM <T>  : MonoBehaviour
//=========================================================================