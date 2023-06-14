public interface ICharacterHealth
{
    public void TakeDamage(int amount);
    public void Die();
    public float GetCurrentHealth();
    public float GetMaxHealth();
}